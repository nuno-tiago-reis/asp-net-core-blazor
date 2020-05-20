using Memento.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Database.Models.Movies
{
	/// <summary>
	/// Implements the interface for a 'Movie' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Movie" />
	/// <seealso cref="MovieFilter" />
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="ModelFilterOrderDirection" />
	public sealed class MovieRepository : ModelRepository<Movie, MovieFilter, MovieFilterOrderBy, ModelFilterOrderDirection>, IMovieRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieRepository{Movie, MovieFilter, MovieFilterOrderBy, ModelFilterOrderDirection}"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="serviceProvider">The services provider.</param>
		/// <param name="logger">The logger.</param>
		public MovieRepository
		(
			MovieContext context,
			ILookupNormalizer lookupNormalizer,
			IServiceProvider serviceProvider,
			ILogger<MovieRepository> logger
		)
		: base(context, lookupNormalizer, serviceProvider, logger)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] IModelRepository
		/// <inheritdoc />
		public async override Task<Movie> CreateAsync(Movie movie)
		{
			return await base.CreateAsync(movie);
		}

		/// <inheritdoc />
		public async override Task<Movie> UpdateAsync(Movie movie)
		{
			return await base.UpdateAsync(movie);
		}

		/// <inheritdoc />
		public async override Task DeleteAsync(long movieId)
		{
			await base.DeleteAsync(movieId);
		}

		/// <inheritdoc />
		public async override Task<Movie> GetAsync(long movieId)
		{
			return await base.GetAsync(movieId);
		}

		/// <inheritdoc />
		public async override Task<IModelPage<Movie>> GetAllAsync(MovieFilter movieFilter = null)
		{
			await Task.Delay(2500);

			var movies = new System.Collections.Generic.List<Movie>
			{
				new Movie
				{
					Title = "Iron Man",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 2, 2008")
				},
				new Movie
				{
					Title = "The Incredible Hulk",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("June 13, 2008")
				},
				new Movie
				{
					Title = "Iron Man 2",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 7, 2010")
				},
				new Movie
				{
					Title = "Thor",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("May 6, 2011")
				},
				new Movie
				{
					Title = "Captain America: The First Avenger",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("July 22, 2011")
				},
				new Movie
				{
					Title = "Marvel's The Avengers",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 4, 2012")
				},
				new Movie
				{
					Title = "Iron Man 3",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 3, 2013")
				},
				new Movie
				{
					Title = "Thor: The Dark World",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 8, 2013")
				},
				new Movie
				{
					Title = "Captain America: The Winter Soldier",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("April 4, 2014")
				},
				new Movie
				{
					Title = "Guardians of the Galaxy",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("August 1, 2014")
				},
				new Movie
				{
					Title = "Avengers: Age of Ultron",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 1, 2015")
				},
				new Movie
				{
					Title = "Ant-Man",
					Genre = "Heist",
					ReleaseDate = DateTime.Parse("July 17, 2015")
				},
				new Movie
				{
					Title = "Captain America: Civil War",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("May 6, 2016")
				},
				new Movie
				{
					Title = "Doctor Strange",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 4, 2016")
				},
				new Movie
				{
					Title = "Guardians of the Galaxy Vol.2",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("May 5, 2017")
				},
				new Movie
				{
					Title = "Spider-Man: Homecoming",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("July 7, 2017")
				},
				new Movie
				{
					Title = "Thor: Ragnarok",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 3, 2017")
				},
				new Movie
				{
					Title = "Black Panther",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("February 16, 2018")
				},
				new Movie
				{
					Title = "Avengers: Infinity War",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("April 27, 2018")
				},
				new Movie
				{
					Title = "Ant-Man and the Wasp",
					Genre = "Heist",
					ReleaseDate = DateTime.Parse("July 6, 2018")
				},
				new Movie
				{
					Title = "Captain Marvel",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("March 8, 2019")
				},
				new Movie
				{
					Title = "Avengers: Endgame",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("April 26, 2019")
				},
				new Movie
				{
					Title = "Spider-Man: Far From Home",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("July 2, 2019")
				}
			};
			var moviePage = ModelPage<Movie>.CreateUnmodified(movies, movies.Count, 1, 100, null, null);

			return moviePage;

			//return await base.GetAllAsync(movieFilter);
		}

		/// <inheritdoc />
		public async override Task<bool> ExistsAsync(long movieId)
		{
			return await base.ExistsAsync(movieId);
		}
		#endregion

		#region [Methods] IMovieRepository
		#endregion

		#region [Methods] Utility
		/// <inheritdoc />
		protected override void NormalizeModel(Movie movie)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void ValidateModel(Movie movie)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void UpdateModel(Movie sourceMovie, Movie targetMovie)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetCountQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetSimpleQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetDetailedQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override void FilterQueryable(IQueryable<Movie> movieQueryable, MovieFilter movieFilter)
		{
			// Nothing to do here.
		}
		#endregion
	}
}