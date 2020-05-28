using AutoMapper;
using Blazor.FileReader;
using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Shared.Configuration;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Services.Http;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Toaster;
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
			builder.RootComponents
				.Add<App>("app");

			builder.Services
				.AddOptions();
			builder.Services
				.AddSharedLocalization<SharedResources>(options =>
				{
					options.DefaultCulture = "en";
					options.SupportedCultures = new[] { "en", "pt" };
				});
			#endregion

			#region [Required: AutoMapper]
			builder.Services
				.AddAutoMapper(typeof(MovieMapperProfile).Assembly);
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

			#region [Required: FileReader]
			builder.Services
				.AddFileReaderService(options =>
				{
					options.InitializeOnFirstCall = true;
				});
			#endregion

			#region [Required: Toastr]
			builder.Services
				.AddToasterService();
			#endregion
		}
		#endregion
	}
}