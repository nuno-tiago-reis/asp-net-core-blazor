using AutoMapper;
using Blazor.FileReader;
using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Shared.Configuration;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Handlers;
using Memento.Shared.Services.Http;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Toaster;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Memento.Movies.Client
{
	/// <summary>
	/// Implements the applications configuration class.
	/// </summary>
	public static class Startup
	{
		#region [Constants]
		/// <summary>
		/// Get http client name.
		/// </summary>
		private static readonly string HttpClientName = typeof(Startup).Assembly.FullName;
		#endregion

		#region [Methods]
		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to add services to the container.
		/// </summary>
		/// 
		/// <param name="builder">The builder.</param>
		public static void ConfigureBuilder(WebAssemblyHostBuilder builder)
		{
			#region [Required: Blazor Components]
			builder.RootComponents
				.Add<App>("app");

			builder.Services
				.AddOptions()
				.AddSharedLocalization<SharedResources>(options =>
				{
					options.DefaultCulture = "en";
					options.SupportedCultures = new[] { "en", "pt" };
				});
			#endregion

			#region [Required: Blazor Authentication]
			builder.Services
				.AddOidcAuthentication(options =>
				{
					var settings = new MovieSettings();
					
					builder.Configuration.Bind(settings);

					// Authority
					options.ProviderOptions.Authority = settings.IdentityClientOptions.Authority;
					options.ProviderOptions.ClientId = settings.IdentityClientOptions.ClientId;

					// Redirection
					options.ProviderOptions.RedirectUri = settings.IdentityClientOptions.RedirectUri;
					options.ProviderOptions.PostLogoutRedirectUri = settings.IdentityClientOptions.PostLogoutRedirectUri;

					// Response
					options.ProviderOptions.ResponseMode = settings.IdentityClientOptions.ResponseMode;
					options.ProviderOptions.ResponseType = settings.IdentityClientOptions.ResponseType;

					// Scopes
					foreach (var scope in settings.IdentityClientOptions.Scopes)
					{
						options.ProviderOptions.DefaultScopes.Add(scope);
					}
				});
			#endregion

			#region [Required: AutoMapper]
			builder.Services
				.AddAutoMapper(typeof(MovieMapperProfile).Assembly);
			#endregion

			#region [Required: Http Clients]
			builder.Services
				.AddHttpClient(HttpClientName, client =>
				{
					client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
				})
				.ConfigureHttpMessageHandlerBuilder(options =>
				{
					var settings = options.Services.GetService<IConfiguration>().Get<MovieSettings>();

					// Convert the enumerables to lists
					var blackListedUris = settings.IdentityClientOptions.BlackListedUris?.ToList() ?? new List<string>();
					var whiteListedUris = settings.IdentityClientOptions.WhiteListedUris?.ToList() ?? new List<string>();

					// Append the base address to the configured uris
					blackListedUris = blackListedUris
						.Select(uri => $"{builder.HostEnvironment.BaseAddress}{uri.ToLowerInvariant()}")
						.ToList();
					whiteListedUris = whiteListedUris
						.Select(uri => $"{builder.HostEnvironment.BaseAddress}{uri.ToLowerInvariant()}")
						.ToList();

					// Make sure there's at least one white-listed uri
					if (whiteListedUris.Count == 0)
					{
						whiteListedUris.Add(builder.HostEnvironment.BaseAddress);
					}

					// Configure the handler
					var handler = new AuthorizationMessageHandler(options.Services);
					handler.ConfigureHandler(blackListedUris, whiteListedUris, settings.IdentityClientOptions.Scopes);

					// Assign the handler
					options.PrimaryHandler = handler;
				});

			builder.Services
				.AddTransient(provider =>
				{
					var factory = provider.GetRequiredService<IHttpClientFactory>();

					return factory.CreateClient(HttpClientName);
				});
			#endregion

			#region [Required: Http Services]
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

			#region [Required: Toaster]
			builder.Services
				.AddToasterService();
			#endregion
		}
		#endregion
	}
}