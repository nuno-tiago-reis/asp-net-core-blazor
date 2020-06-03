using Memento.Movies.Shared.Resources;
using Memento.Shared.Exceptions;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Repositories;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Movies
{
	/// <summary>
	/// Implements the interface for a 'Movie' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Movie" />
	/// <seealso cref="MovieFilter" />
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="MovieFilterOrderDirection" />
	public sealed class MovieRepository : ModelRepository<Movie, MovieFilter, MovieFilterOrderBy, MovieFilterOrderDirection>, IMovieRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieRepository"/> class.
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
		public override async Task<Movie> CreateAsync(Movie movie)
		{
			return await base.CreateAsync(movie);
		}

		/// <inheritdoc />
		public override async Task<Movie> UpdateAsync(Movie movie)
		{
			return await base.UpdateAsync(movie);
		}

		/// <inheritdoc />
		public override async Task DeleteAsync(long movieId)
		{
			await base.DeleteAsync(movieId);
		}

		/// <inheritdoc />
		public override async Task<Movie> GetAsync(long movieId)
		{
			return await base.GetAsync(movieId);
		}

		/// <inheritdoc />
		public override async Task<IPage<Movie>> GetAllAsync(MovieFilter movieFilter = null)
		{
			return await base.GetAllAsync(movieFilter);
		}

		/// <inheritdoc />
		public override async Task<bool> ExistsAsync(long movieId)
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
			if (this.Models.Any(movie => movie.Id != sourceMovie.Id && movie.NormalizedName.Equals(sourceMovie.NormalizedName) && movie.ReleaseDate == sourceMovie.ReleaseDate))
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
			// Movie
			targetMovie.Name = sourceMovie.Name;
			targetMovie.NormalizedName = sourceMovie.NormalizedName;
			targetMovie.Summary = sourceMovie.Summary;
			targetMovie.PictureUrl = sourceMovie.PictureUrl;
			targetMovie.TrailerUrl = sourceMovie.TrailerUrl;
			targetMovie.ReleaseDate = sourceMovie.ReleaseDate;
			targetMovie.InTheaters = sourceMovie.InTheaters;

			// Model
			targetMovie.CreatedAt = sourceMovie.CreatedAt;
			targetMovie.CreatedBy = sourceMovie.CreatedBy;
			targetMovie.UpdatedAt = sourceMovie.UpdatedAt;
			targetMovie.UpdatedBy = sourceMovie.UpdatedBy;
		}

		/// <inheritdoc />
		protected override void UpdateModelRelations(Movie sourceMovie, Movie targetMovie)
		{
			// Check which genres need to be added
			if (sourceMovie.Genres != null)
			{
				// Clear duplicates
				sourceMovie.Genres = sourceMovie.Genres
					.GroupBy(genre => new { genre.GenreId }, (key, genres) => genres.FirstOrDefault())
					.ToList();

				// Cross check the source with the target
				foreach (var sourceGenre in sourceMovie.Genres)
				{
					if (targetMovie.Genres == null || targetMovie.Genres.All(genre => genre.GenreId != sourceGenre.GenreId))
					{
						// Make sure the link is there
						sourceGenre.MovieId = targetMovie.Id;

						// Add the connection to the context
						this.Context.Add(sourceGenre);
						continue;
					}
				}
			}

			// Check which genres need to be removed
			if (targetMovie.Genres != null)
			{
				// Cross check the source with the target
				foreach (var targetGenre in targetMovie.Genres)
				{
					if (sourceMovie.Genres == null || sourceMovie.Genres.All(genre => genre.GenreId != targetGenre.GenreId))
					{
						// Remove the connection from the context
						this.Context.Remove(targetGenre);
						continue;
					}
				}
			}

			// Check which persons need to be added
			if (sourceMovie.Persons != null)
			{
				// Clear duplicates
				sourceMovie.Persons = sourceMovie.Persons
					.GroupBy(person => new { person.PersonId, person.Role }, (key, persons) => persons.FirstOrDefault())
					.ToList();

				// Cross check the source with the target
				foreach (var sourcePerson in sourceMovie.Persons)
				{
					if (targetMovie.Persons == null || targetMovie.Persons.All(person => person.PersonId != sourcePerson.PersonId || person.Role != sourcePerson.Role))
					{
						// Make sure the link is there
						sourcePerson.MovieId = targetMovie.Id;

						// Add the connection to the context
						this.Context.Add(sourcePerson);
						continue;
					}
				}
			}

			// Check which persons need to be removed
			if (targetMovie.Persons != null)
			{
				// Cross check the source with the target
				foreach (var targetPerson in targetMovie.Persons)
				{
					if (sourceMovie.Persons == null || sourceMovie.Persons.All(person => person.PersonId != targetPerson.PersonId && person.Role != targetPerson.Role))
					{
						// Remove the connection from the context
						this.Context.Remove(targetPerson);
						continue;
					}
				}
			}
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
				.ThenInclude(movieGenre => movieGenre.Genre)
				.Include(movie => movie.Persons)
				.ThenInclude(moviePerson => moviePerson.Person);
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> GetDetailedQueryable()
		{
			return this.Models
				.Include(movie => movie.Genres)
				.ThenInclude(movieGenre => movieGenre.Genre)
				.Include(movie => movie.Persons)
				.ThenInclude(moviePerson => moviePerson.Person);
		}

		/// <inheritdoc />
		protected override IQueryable<Movie> FilterQueryable(IQueryable<Movie> movieQueryable, MovieFilter movieFilter)
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
				switch (movieFilter.InTheaters)
				{
					case MovieFilterInTheaters.Checked:
					{
						movieQueryable = movieQueryable.Where(movie => movie.InTheaters == true);
						break;
					}


					case MovieFilterInTheaters.Unchecked:
					{
						movieQueryable = movieQueryable.Where(movie => movie.InTheaters == false);
						break;
					}
				}
			}

			// Apply the order
			switch (movieFilter.OrderBy)
			{
				case MovieFilterOrderBy.Id:
				{
					movieQueryable = OrderQueryable(movieQueryable, movieFilter, movie => movie.Id);
					break;
				}

				case MovieFilterOrderBy.Name:
				{
					movieQueryable = OrderQueryable(movieQueryable, movieFilter, movie => movie.Name);
					break;
				}

				case MovieFilterOrderBy.ReleaseDate:
				{
					movieQueryable = OrderQueryable(movieQueryable, movieFilter, movie => movie.ReleaseDate);
					break;
				}

				case MovieFilterOrderBy.CreatedAt:
				{
					movieQueryable = OrderQueryable(movieQueryable, movieFilter, movie => movie.CreatedAt);
					break;
				}

				case MovieFilterOrderBy.UpdatedAt:
				{
					movieQueryable = OrderQueryable(movieQueryable, movieFilter, movie => movie.UpdatedAt);
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException(nameof(movieFilter.OrderBy));
				}
			}

			return movieQueryable;
		}

		/// <summary>
		/// Returns an ordered queryable according to the filters OrderDirection and expressions Property.
		/// </summary>
		///
		/// <typeparam name="TProperty">The property's type.</typeparam>
		///
		/// <param name="movieQueryable">The movie queryable.</param>
		/// <param name="movieFilter">The movie filter.</param>
		/// <param name="movieExpression">The movie expression</param>
		private static IQueryable<Movie> OrderQueryable<TProperty>(IQueryable<Movie> movieQueryable, MovieFilter movieFilter, Expression<Func<Movie, TProperty>> movieExpression)
		{
			switch (movieFilter.OrderDirection)
			{
				case MovieFilterOrderDirection.Ascending:
				{
					return movieQueryable.OrderBy(movieExpression);
				}
				case MovieFilterOrderDirection.Descending:
				{
					return movieQueryable.OrderByDescending(movieExpression);
				}
				default:
				{
					throw new ArgumentOutOfRangeException(nameof(movieFilter.OrderDirection));
				}
			}
		}
		#endregion

		#region [Methods] Messages
		/// <inheritdoc />
		protected override string GetModelDoesNotExistMessage()
		{
			// Get the name
			var name = this.Localizer.GetString(SharedResources.MOVIE);

			return this.Localizer.GetString(SharedResources.ERROR_NOT_FOUND, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasDuplicateFieldMessage<TProperty>(Expression<Func<Movie, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_DUPLICATE_FIELD, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasInvalidFieldMessage<TProperty>(Expression<Func<Movie, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_INVALID_FIELD, name);
		}
		#endregion
	}
}