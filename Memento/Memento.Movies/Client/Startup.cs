using AutoMapper;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Genres;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Memento.Movies.Client
{
	/// <summary>
	/// Implements the applications configuration class.
	/// </summary>
	public static class Startup
	{
		#region [Methods]
		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to add services to the container.
		/// </summary>
		/// 
		/// <param name="services">The services.</param>
		public static void ConfigureBuilder(WebAssemblyHostBuilder builder)
		{
			#region [Required: Blazor Components]
			// Components
			builder.RootComponents
				.Add<App>("app");

			// Configurations
			builder.Services
				.AddOptions();
			#endregion

			#region [Required: Blazor Localization]
			// Middleware
			builder.Services
				.AddLocalization();

			// Configurations
			builder.Services
				.Configure<RequestLocalizationOptions>(options =>
				{
					var localizationOptions = LocalizationSettings.GetLocalizationOptions();

					options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
					options.SupportedCultures = localizationOptions.SupportedCultures;
					options.SupportedUICultures = localizationOptions.SupportedUICultures;
				});
			#endregion

			#region [Required: Blazor EntityFramework]
			builder.Services
				.AddDbContext<MovieContext>(options =>
			{
				options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Memento.Movies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			})
				.AddTransient<MovieSeeder>();

			builder.Services
				.AddTransient<IGenreRepository, GenreRepository>()
				.AddTransient<IMovieRepository, MovieRepository>()
				.AddTransient<IPersonRepository, PersonRepository>();
			#endregion

			#region [Required: AutoMapper]
			builder.Services
				.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			#endregion

			#region [Required: HttpClients]
			builder.Services
				.AddTransient(provider => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			#endregion

			#region [Required: Services]
			builder.Services
				.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
			#endregion
		}
		#endregion
	}
}