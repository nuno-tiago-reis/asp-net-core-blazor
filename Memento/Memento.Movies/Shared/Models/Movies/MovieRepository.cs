﻿using Memento.Shared.Models;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Implements the interface for a 'Movie' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Movie" />
	/// <seealso cref="MovieFilter" />
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class MovieRepository : ModelRepository<Movie, MovieFilter, MovieFilterOrderBy, FilterOrderDirection>, IMovieRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieRepository{Movie, MovieFilter, MovieFilterOrderBy, FilterOrderDirection}"/> class.
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
		public async override Task<IPage<Movie>> GetAllAsync(MovieFilter movieFilter = null)
		{
			await Task.Delay(2500);

			var movies = new System.Collections.Generic.List<Movie>
			{
				new Movie
				{
					Name ="Iron Man",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTczNTI2ODUwOF5BMl5BanBnXkFtZTcwMTU0NTIzMw@@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 2, 2008")
				},
				new Movie
				{
					Name ="The Incredible Hulk",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTUyNzk3MjA1OF5BMl5BanBnXkFtZTcwMTE1Njg2MQ@@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("June 13, 2008")
				},
				new Movie
				{
					Name ="Iron Man 2",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTM0MDgwNjMyMl5BMl5BanBnXkFtZTcwNTg3NzAzMw@@._V1_.jpg",
					ReleaseDate = DateTime.Parse("May 7, 2010")
				},
				new Movie
				{
					Name ="Thor",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BOGE4NzU1YTAtNzA3Mi00ZTA2LTg2YmYtMDJmMThiMjlkYjg2XkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 6, 2011")
				},
				new Movie
				{
					Name ="Captain America: The First Avenger",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTYzOTc2NzU3N15BMl5BanBnXkFtZTcwNjY3MDE3NQ@@._V1_SY1000_CR0,0,640,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("July 22, 2011")
				},
				new Movie
				{
					Name ="Marvel's The Avengers",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 4, 2012")
				},
				new Movie
				{
					Name ="Iron Man 3",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMjE5MzcyNjk1M15BMl5BanBnXkFtZTcwMjQ4MjcxOQ@@._V1_SY1000_SX700_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 3, 2013")
				},
				new Movie
				{
					Name ="Thor: The Dark World",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTQyNzAwOTUxOF5BMl5BanBnXkFtZTcwMTE0OTc5OQ@@._V1_SY1000_SX700_AL_.jpg",
					ReleaseDate = DateTime.Parse("November 8, 2013")
				},
				new Movie
				{
					Name ="Captain America: The Winter Soldier",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMzA2NDkwODAwM15BMl5BanBnXkFtZTgwODk5MTgzMTE@._V1_SY1000_CR0,0,685,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("April 4, 2014")
				},
				new Movie
				{
					Name ="Guardians of the Galaxy",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTAwMjU5OTgxNjZeQTJeQWpwZ15BbWU4MDUxNDYxODEx._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("August 1, 2014")
				},
				new Movie
				{
					Name ="Avengers: Age of Ultron",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_SY1000_SX675_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 1, 2015")
				},
				new Movie
				{
					Name ="Ant-Man",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMjM2NTQ5Mzc2M15BMl5BanBnXkFtZTgwNTcxMDI2NTE@._V1_.jpg",
					ReleaseDate = DateTime.Parse("July 17, 2015")
				},
				new Movie
				{
					Name ="Captain America: Civil War",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMjQ0MTgyNjAxMV5BMl5BanBnXkFtZTgwNjUzMDkyODE@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("May 6, 2016")
				},
				new Movie
				{
					Name ="Doctor Strange",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BNjgwNzAzNjk1Nl5BMl5BanBnXkFtZTgwMzQ2NjI1OTE@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("November 4, 2016")
				},
				new Movie
				{
					Name ="Guardians of the Galaxy Vol.2",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BNjM0NTc0NzItM2FlYS00YzEwLWE0YmUtNTA2ZWIzODc2OTgxXkEyXkFqcGdeQXVyNTgwNzIyNzg@._V1_.jpg",
					ReleaseDate = DateTime.Parse("May 5, 2017")
				},
				new Movie
				{
					Name ="Spider-Man: Homecoming",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BNTk4ODQ1MzgzNl5BMl5BanBnXkFtZTgwMTMyMzM4MTI@._V1_SY1000_CR0,0,658,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("July 7, 2017")
				},
				new Movie
				{
					Name ="Thor: Ragnarok",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMjMyNDkzMzI1OF5BMl5BanBnXkFtZTgwODcxODg5MjI@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("November 3, 2017")
				},
				new Movie
				{
					Name ="Black Panther",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("February 16, 2018")
				},
				new Movie
				{
					Name ="Avengers: Infinity War",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMjMxNjY2MDU1OV5BMl5BanBnXkFtZTgwNzY1MTUwNTM@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("April 27, 2018")
				},
				new Movie
				{
					Name ="Ant-Man and the Wasp",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BYjcyYTk0N2YtMzc4ZC00Y2E0LWFkNDgtNjE1MzZmMGE1YjY1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("July 6, 2018")
				},
				new Movie
				{
					Name ="Captain Marvel",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTE0YWFmOTMtYTU2ZS00ZTIxLWE3OTEtYTNiYzBkZjViZThiXkEyXkFqcGdeQXVyODMzMzQ4OTI@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("March 8, 2019")
				},
				new Movie
				{
					Name ="Avengers: Endgame",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("April 26, 2019")
				},
				new Movie
				{
					Name ="Spider-Man: Far From Home",
					PictureUrl = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_SY1000_CR0,0,674,1000_AL_.jpg",
					ReleaseDate = DateTime.Parse("July 2, 2019")
				}
			};
			var moviePage = Page<Movie>.CreateUnmodified(movies, movies.Count, 1, 100, null, null);

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