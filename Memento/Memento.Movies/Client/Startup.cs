using Memento.Movies.Shared.Database;
using Memento.Movies.Shared.Database.Models.Movies;
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
			// Components
			builder.RootComponents.Add<App>("app");

			// Options
			builder.Services.AddOptions();

			// Http Clients
			builder.Services.AddTransient(provider => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			builder.Services.AddDbContext<MovieContext>(options =>
			{
				options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Memento.Movies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			});

			// Repositories
			builder.Services.AddTransient<IMovieRepository, MovieRepository>();

			builder.Services.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
		}
		#endregion
	}
}