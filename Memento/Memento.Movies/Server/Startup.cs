using AutoMapper;
using Memento.Movies.Shared.Configuration;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Bindings;
using Memento.Shared.Routing;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Storage;
using Memento.Shared.Services.Toaster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Memento.Movies.Server
{
	/// <summary>
	/// Implements the applications configuration class.
	/// </summary>
	public sealed class Startup
	{
		#region [Properties]
		/// <summary>
		/// The configuration.
		/// </summary>
		private readonly IConfiguration Configuration;

		/// <summary>
		/// Gets the movie settings.
		/// </summary>
		private readonly MovieSettings MovieSettings;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// 
		/// <param name="configuration">The configuration.</param>
		/// <param name="environment">The environment.</param> 
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
			this.MovieSettings = configuration.Get<MovieSettings>();
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to add services to the container.
		/// </summary>
		/// 
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			#region [Required: ASP.NET AppSettings]
			services
				.Configure<MovieSettings>(this.Configuration);
			#endregion

			#region [Required: ASP.NET Middleware]
			services
				.AddControllers()
				.AddSharedLocalization<SharedResources>(this.MovieSettings.Localization);

			services
				.Configure<ApiBehaviorOptions>(options =>
				{
					// hide the default validation errors
					options.InvalidModelStateResponseFactory = (context) =>
					{
						foreach (var keyValuePair in context.ModelState)
						{
							if (keyValuePair.Value.ValidationState != ModelValidationState.Invalid)
								continue;

							keyValuePair.Value.Errors.Clear();
							keyValuePair.Value.Errors.Add(new ModelError($"The '{keyValuePair.Key}' field is invalid."));
						}

						return new BadRequestObjectResult(context.ModelState);
					};
					// see: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apibehavioroptions.suppressinferbindingsourcesforparameters?view=aspnetcore-3.1
					options.SuppressInferBindingSourcesForParameters = true;
				});
			services
				.Configure<CookiePolicyOptions>(options =>
				{
					// require the consent cookie
					options.CheckConsentNeeded = context => true;
					// require the latest cookie site policies
					options.MinimumSameSitePolicy = SameSiteMode.None;
				});
			services
				.Configure<MvcOptions>(options =>
				{
					// transform the routing tokens by splitting them with slashes
					options.Conventions.Add(new RouteTokenTransformerConvention(new SlashParameterTransformer()));
					// convert datetimes to utc
					options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
				});
			services
				.Configure<JsonOptions>(options =>
				{
					// configure the default options
					options.JsonSerializerOptions.ConfigureDefaultOptions();
				});
			services
				.Configure<RouteOptions>(options =>
				{
					// transform the routing tokens by converting them to lower case
					options.LowercaseUrls = true;
					// don't append a trailing slash
					options.AppendTrailingSlash = false;
				});
			#endregion

			#region [Required: ASP.NET DataProtection]
			//services
				//.AddAzureDataProtection(this.MovieSettings.DataProtection);
				//.AddFileSystemDataProtection(this.MovieSettings.DataProtection);
			#endregion

			#region [Required: ASP.NET EntityFramework]
			services
				.AddDbContext<MovieContext>(options =>
				{
					options.UseSqlServer(this.MovieSettings.ConnectionStrings.DefaultConnection, builder =>
					{
						builder.MigrationsAssembly(typeof(MovieContext).Assembly.FullName);
					});
				})
				.AddTransient<MovieSeeder>();

			services
				.AddTransient<IGenreRepository, GenreRepository>()
				.AddTransient<IMovieRepository, MovieRepository>()
				.AddTransient<IPersonRepository, PersonRepository>();
			#endregion

			#region [Required: ASP.NET Services]
			services
				.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
			#endregion

			#region [Required: AutoMapper]
			services
				.AddAutoMapper(typeof(MovieMapperProfile).Assembly);
			#endregion

			#region [Required: Services]
			services
				.AddToasterService()
				//.AddAzureStorageService(this.MovieSettings.Storage);
				.AddFileSystemStorageService(this.MovieSettings.Storage);
			#endregion
		}

		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// 
		/// <param name="builder">The builder.</param>
		/// <param name="environment">The environment.</param>
		// 
		public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
		{
			#region [Required: AspNet Errors]
			if (environment.IsDevelopment())
			{
				builder.UseDeveloperExceptionPage();
				builder.UseWebAssemblyDebugging();
			}
			else
			{
				builder.UseExceptionHandler("/Error");
				builder.UseWebAssemblyDebugging();
			}
			#endregion

			#region [Required: AspNet Middleware]
			builder.UseCors(action =>
			{
				action.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
			builder.UseHttpsRedirection();
			builder.UseBlazorFrameworkFiles();
			builder.UseCookiePolicy();
			builder.UseStaticFiles();
			builder.UseRouting();
			builder.UseHsts();
			#endregion

			#region [Required: AspNet Localization]
			builder.UseSharedLocalization(this.MovieSettings.Localization);
			#endregion

			#region [Required: AspNet EntityFramework]
			using (var scope = builder.ApplicationServices.CreateScope())
			{
				scope.ServiceProvider.GetService<MovieContext>().Database.Migrate();
				scope.ServiceProvider.GetService<MovieSeeder>().Seed();
			}
			#endregion

			#region [Required: AspNet Routing]
			builder.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
			#endregion
		}
		#endregion
	}
}