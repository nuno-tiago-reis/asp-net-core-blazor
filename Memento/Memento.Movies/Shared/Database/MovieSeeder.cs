using Memento.Movies.Shared.Database.Models.Movies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Memento.Movies.Shared.Database
{
	/// <summary>
	/// Implements the movie database seeder.
	/// </summary>
	///
	/// <seealso cref="MovieContext"/>
	public sealed class MovieSeeder
	{
		#region [Constants]
		/// <summary>
		/// The filename with the seeding data for the 'Movie' models.
		/// </summary>
		private const string MOVIES_FILE_NAME = "Database/Models/Seeding/Movies";
		#endregion

		#region [Properties]
		/// <summary>
		/// The context.
		/// </summary>
		private readonly MovieContext Context;

		/// <summary>
		/// The hosting environment.
		/// </summary>
		private readonly IHostingEnvironment Environment;

		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger Logger;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieSeeder"/> class.
		/// </summary>
		/// 
		/// <param name="context">The identity context.</param
		/// <param name="environment">The environment.</param>
		/// <param name="logger">The logger.</param>
		public MovieSeeder
		(
			MovieContext context,
			IHostingEnvironment environment,
			ILogger<MovieSeeder> logger
		)
		{
			this.Context = context;
			this.Environment = environment;
			this.Logger = logger;
		}
		#endregion

		#region [Seed Methods]
		/// <summary>
		/// Seeds the movie context models (as well as other necessary entities).
		/// </summary>
		public void Seed()
		{
			this.SeedMovies();
		}

		/// <summary>
		/// Seeds the movies.
		/// </summary>
		private void SeedMovies()
		{
			// Build the movies
			var movies = new List<Movie>();

			try
			{
				// Read the movies from the global file
				string globalFile = $"{MOVIES_FILE_NAME}.json";
				movies.AddRange(JsonSerializer.Deserialize<List<Movie>>(File.ReadAllText(globalFile)));
			}
			catch (DirectoryNotFoundException)
			{
				// Ignore if the file does not exist
			}
			catch (Exception exception)
			{
				this.Logger.LogError(exception.Message, exception);
			}

			try
			{
				// Read the movies from the environment specific file
				string environmentFile = $"{MOVIES_FILE_NAME}.{this.Environment.EnvironmentName}.json";
				movies.AddRange(JsonSerializer.Deserialize<List<Movie>>(File.ReadAllText(environmentFile)));
			}
			catch (DirectoryNotFoundException)
			{
				// Ignore if the file does not exist
			}
			catch (Exception exception)
			{
				this.Logger.LogError(exception.Message, exception);
			}

			// Sort the movies
			movies.Sort((first, second) => string.Compare(first.Title, second.Title, StringComparison.Ordinal));

			// Update the context
			foreach (var movie in movies)
			{
				// Check if it exists
				var contextMovie = this.Context.Movies
					.FirstOrDefault(m => m.Title == movie.Title);

				// Add the movie
				if (contextMovie == null)
				{
					this.Context.Movies.Add(movie);
					continue;
				}
			}

			// Save the context
			this.Context.SaveChanges();
		}
		#endregion
	}
}