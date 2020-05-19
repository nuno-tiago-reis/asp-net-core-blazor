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
			// Components
			builder.RootComponents.Add<App>("app");

			// Options
			builder.Services.AddOptions();

			// Services
			builder.Services.AddTransient(provider => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
		}
		#endregion
	}
}