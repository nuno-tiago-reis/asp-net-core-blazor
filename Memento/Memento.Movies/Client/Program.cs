using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Memento.Movies.Client
{
	/// <summary>
	/// Implements the applications bootstrapping class.
	/// </summary>
	public sealed class Program
	{
		/// <summary>
		/// The applications bootstrapping method.
		/// </summary>
		/// 
		/// <param name="arguments">The bootstrapping arguments.</param>
		public static async Task Main(string[] arguments)
		{
			await CreateWebAssemblyHostBuilder(arguments).Build().RunAsync();
		}

		/// <summary>
		/// The applications host building method.
		/// </summary>
		/// 
		/// <param name="arguments">The bootstrapping arguments.</param>
		public static WebAssemblyHostBuilder CreateWebAssemblyHostBuilder(string[] arguments)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(arguments);

			Startup.ConfigureBuilder(builder);

			return builder;
		}
	}
}