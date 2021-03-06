using AutoMapper;
using Memento.Movies.Shared.Configuration;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Genres;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Bindings;
using Memento.Shared.Routing.Transformers;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
						foreach (var (key, value) in context.ModelState)
						{
							if (value.ValidationState != ModelValidationState.Invalid)
								continue;

							value.Errors.Clear();
							value.Errors.Add(new ModelError($"The '{key}' field is invalid."));
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

			#region [Required: ASP.NET Authentication]
			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.Authority = this.MovieSettings.IdentityResourceOptions.Authority;
					options.Audience = this.MovieSettings.IdentityResourceOptions.Audience;
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
		public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
		{
			#region [Required: ASP.NET Errors]
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

			#region [Required: ASP.NET Middleware]
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

			#region [Required: ASP.NET Authentication]
			builder.UseAuthentication();
			builder.UseAuthorization();
			#endregion

			#region [Required: ASP.NET Localization]
			builder.UseSharedLocalization(this.MovieSettings.Localization);
			#endregion

			#region [Required: ASP.NET EntityFramework]
			using (var scope = builder.ApplicationServices.CreateScope())
			{
				scope.ServiceProvider.GetService<MovieContext>().Database.Migrate();
				scope.ServiceProvider.GetService<MovieSeeder>().Seed();
			}
			#endregion

			#region [Required: ASP.NET Routing]
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