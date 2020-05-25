using AutoMapper;
using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Shared.Configuration;
using Memento.Shared.Configuration;
using Memento.Shared.Localization;
using Memento.Shared.Services.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

			#region [Required: AutoMapper]
			builder.Services
				.AddAutoMapper(typeof(MovieMapperSettings).Assembly);
			#endregion

			#region [Required: HttpClients]
			builder.Services
				.AddTransient(provider => new HttpClient
				{
					BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
				});
			#endregion

			#region [Required: Services]
			builder.Services
				.AddSingleton<IHttpService, HttpService>()
				.AddSingleton<IGenreService, GenreService>()
				.AddSingleton<IMovieService, MovieService>()
				.AddSingleton<IPersonService, PersonService>();
			#endregion

			#region [Required: Toastr]
			builder.Services
				.AddToaster(new ToasterSettings());
			#endregion
		}
		#endregion
	}
}