using Memento.Shared.Exceptions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Repositories;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models.Repositories.Movies
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
		/// <param name="localizer">The localizer.</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="logger">The logger.</param>
		public MovieRepository
		(
			MovieContext context,
			ILocalizerService localizer,
			ILookupNormalizer lookupNormalizer,
			ILogger<MovieRepository> logger
		)
		: base(context, localizer, lookupNormalizer, logger)
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
			return await base.GetAllAsync(movieFilter);
		}

		/// <inheritdoc />
		public async override Task<bool> ExistsAsync(long movieId)
		{
			return await base.ExistsAsync(movieId);
		}
		#endregion

		#region [Methods] IMovieRepository
		#endregion

		#region [Methods] Model
		/// <inheritdoc />
		protected override void NormalizeModel(Movie sourceMovie)
		{
			sourceMovie.NormalizedName = this.LookupNormalizer.NormalizeName(sourceMovie.Name ?? string.Empty);
		}

		/// <inheritdoc />
		protected override void ValidateModel(Movie sourceMovie)
		{
			var errorMessages = new List<string>();

			// Required fields
			if (string.IsNullOrWhiteSpace(sourceMovie.Name))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(movie => movie.Name));
			}
			if (string.IsNullOrWhiteSpace(sourceMovie.Summary))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(movie => movie.Summary));
			}
			if (string.IsNullOrWhiteSpace(sourceMovie.PictureUrl))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(movie => movie.PictureUrl));
			}
			if (string.IsNullOrWhiteSpace(sourceMovie.TrailerUrl))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(movie => movie.TrailerUrl));
			}
			if (sourceMovie.ReleaseDate == default)
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(movie => movie.ReleaseDate));
			}

			// Duplicate fields
			if (this.Models.Any(movie => movie.NormalizedName.Equals(sourceMovie.NormalizedName) && movie.ReleaseDate == sourceMovie.ReleaseDate))
			{
				errorMessages.Add(this.GetModelHasDuplicateFieldMessage(movie => movie.Name));
				errorMessages.Add(this.GetModelHasDuplicateFieldMessage(movie => movie.ReleaseDate));
			}

			if (errorMessages.Count > 0)
			{
				throw new MementoException(errorMessages, MementoExceptionType.BadRequest);
			}
		}

		/// <inheritdoc />
		protected override void UpdateModel(Movie sourceMovie, Movie targetMovie)
		{
			targetMovie.Name = sourceMovie.Name;
			targetMovie.NormalizedName = sourceMovie.NormalizedName;
			targetMovie.Summary = sourceMovie.Summary;
			targetMovie.PictureUrl = sourceMovie.PictureUrl;
			targetMovie.TrailerUrl = sourceMovie.TrailerUrl;
			targetMovie.ReleaseDate = sourceMovie.ReleaseDate;
			targetMovie.InTheaters = sourceMovie.InTheaters;
			targetMovie.Genres = sourceMovie.Genres;
			targetMovie.Persons = sourceMovie.Persons;
		}
		#endregion

		#region [Methods] Queryable
		/// <inheritdoc />
		protected override IQueryable<Movie> GetCountQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetSimpleQueryable()
		{
			return this.Models
				.Include(movie => movie.Genres)
				.Include(movie => movie.Persons);
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetDetailedQueryable()
		{
			return this.Models
				.Include(movie => movie.Genres)
				.Include(movie => movie.Persons);
		}

		/// <inheritdoc />
		protected override void FilterQueryable(IQueryable<Movie> movieQueryable, MovieFilter movieFilter)
		{
			// Apply the filter
			if (string.IsNullOrWhiteSpace(movieFilter.Name) == false)
			{
				var name = this.LookupNormalizer.NormalizeName(movieFilter.Name);

				movieQueryable = movieQueryable.Where(movie => EF.Functions.Like(movie.Name, $"%{name}%"));
			}

			if (string.IsNullOrWhiteSpace(movieFilter.Summary) == false)
			{
				var summary = movieFilter.Summary;

				movieQueryable = movieQueryable.Where(movie => EF.Functions.Like(movie.Summary, $"%{summary}%"));
			}

			if (movieFilter.ReleasedAfter != null)
			{
				var releaseDate = movieFilter.ReleasedAfter.Value;

				movieQueryable = movieQueryable.Where(movie => movie.ReleaseDate >= releaseDate);
			}

			if (movieFilter.ReleasedBefore != null)
			{
				var releaseDate = movieFilter.ReleasedBefore.Value;

				movieQueryable = movieQueryable.Where(movie => movie.ReleaseDate <= releaseDate);
			}

			if (movieFilter.InTheaters != null)
			{
				var inThreaters = movieFilter.InTheaters.Value;

				movieQueryable = movieQueryable.Where(movie => movie.InTheaters == inThreaters);
			}

			// Apply the order
			switch (movieFilter.OrderBy)
			{
				case MovieFilterOrderBy.Id:
				{
					movieQueryable = movieFilter.OrderDirection == FilterOrderDirection.Ascending
						? movieQueryable.OrderBy(movie => movie.Id)
						: movieQueryable.OrderByDescending(movie => movie.Id);
					break;
				}

				case MovieFilterOrderBy.Name:
				{
					movieQueryable = movieFilter.OrderDirection == FilterOrderDirection.Ascending
						? movieQueryable.OrderBy(movie => movie.Name)
						: movieQueryable.OrderByDescending(movie => movie.Name);
					break;
				}

				case MovieFilterOrderBy.CreatedAt:
				{
					movieQueryable = movieFilter.OrderDirection == FilterOrderDirection.Ascending
						? movieQueryable.OrderBy(movie => movie.CreatedAt)
						: movieQueryable.OrderByDescending(movie => movie.CreatedAt);
					break;
				}

				case MovieFilterOrderBy.UpdatedAt:
				{
					movieQueryable = movieFilter.OrderDirection == FilterOrderDirection.Ascending
						? movieQueryable.OrderBy(movie => movie.UpdatedAt)
						: movieQueryable.OrderByDescending(movie => movie.UpdatedAt);
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException(nameof(movieFilter.OrderBy));
				}
			}
		}
		#endregion
	}
}