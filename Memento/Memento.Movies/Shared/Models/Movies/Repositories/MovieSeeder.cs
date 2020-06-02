using Memento.Movies.Shared.Models.Movies.Repositories.Genres;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Memento.Movies.Shared.Models.Movies
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
		/// The filename with the seeding data for the 'Genre' models.
		/// </summary>
		private const string GENRE_FILE_NAME = "Models/Movies/Seeding/Genres";

		/// <summary>
		/// The filename with the seeding data for the 'Movie' models.
		/// </summary>
		private const string MOVIES_FILE_NAME = "Models/Movies/Seeding/Movies";

		/// <summary>
		/// The filename with the seeding data for the 'Person' models.
		/// </summary>
		private const string PERSONS_FILE_NAME = "Models/Movies/Seeding/Persons";
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
			this.SeedGenres();
			this.SeedMovies();
			this.SeedPersons();
		}

		/// <summary>
		/// Seeds the genres.
		/// </summary>
		private void SeedGenres()
		{
			// Build the genres
			var genres = new List<Genre>();

			try
			{
				// Read the genres from the global file
				string globalFile = $"{GENRE_FILE_NAME}.json";
				genres.AddRange(JsonSerializer.Deserialize<List<Genre>>(File.ReadAllText(globalFile)));
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
				// Read the genres from the environment specific file
				string environmentFile = $"{GENRE_FILE_NAME}.{this.Environment.EnvironmentName}.json";
				genres.AddRange(JsonSerializer.Deserialize<List<Genre>>(File.ReadAllText(environmentFile)));
			}
			catch (DirectoryNotFoundException)
			{
				// Ignore if the file does not exist
			}
			catch (Exception exception)
			{
				this.Logger.LogError(exception.Message, exception);
			}

			// Sort the genres
			genres.Sort((first, second) => string.Compare(first.Name, second.Name, StringComparison.Ordinal));

			// Update the context
			foreach (var genre in genres)
			{
				// Check if it exists
				var contextGenre = this.Context.Genres
					.FirstOrDefault(g => g.Name == genre.Name);

				// Add the genre
				if (contextGenre == null)
				{
					this.Context.Genres.Add(genre);
					continue;
				}
			}

			// Save the context
			this.Context.SaveChanges();
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
			movies.Sort((first, second) => first.ReleaseDate.CompareTo(second.ReleaseDate));

			// Update the context
			foreach (var movie in movies)
			{
				// Check if it exists
				var contextMovie = this.Context.Movies
					.FirstOrDefault(m => m.Name == movie.Name && m.ReleaseDate == movie.ReleaseDate);

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

		/// <summary>
		/// Seeds the persons.
		/// </summary>
		private void SeedPersons()
		{
			// Build the persons
			var persons = new List<Person>();

			try
			{
				// Read the persons from the global file
				string globalFile = $"{PERSONS_FILE_NAME}.json";
				persons.AddRange(JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(globalFile)));
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
				// Read the persons from the environment specific file
				string environmentFile = $"{PERSONS_FILE_NAME}.{this.Environment.EnvironmentName}.json";
				persons.AddRange(JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(environmentFile)));
			}
			catch (DirectoryNotFoundException)
			{
				// Ignore if the file does not exist
			}
			catch (Exception exception)
			{
				this.Logger.LogError(exception.Message, exception);
			}

			// Sort the persons
			persons.Sort((first, second) => first.BirthDate.CompareTo(second.BirthDate));

			// Update the context
			foreach (var person in persons)
			{
				// Check if it exists
				var contextPerson = this.Context.Persons
					.FirstOrDefault(p => p.Name == person.Name && p.BirthDate == person.BirthDate);

				// Add the person
				if (contextPerson == null)
				{
					this.Context.Persons.Add(person);
					continue;
				}
			}

			// Save the context
			this.Context.SaveChanges();
		}
		#endregion
	}
}