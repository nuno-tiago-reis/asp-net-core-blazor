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

namespace Memento.Movies.Shared.Models.Movies.Repositories.Genres
{
	/// <summary>
	/// Implements the interface for a 'Genre' repository.
	/// Provides methods to interact with the genres (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Genre" />
	/// <seealso cref="GenreFilter" />
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="GenreFilterOrderDirection" />
	public sealed class GenreRepository : ModelRepository<Genre, GenreFilter, GenreFilterOrderBy, GenreFilterOrderDirection>, IGenreRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="GenreRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context</param>
		/// <param name="localizer">The localizer.</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="logger">The logger.</param>
		public GenreRepository
		(
			MovieContext context,
			ILocalizerService localizer,
			ILookupNormalizer lookupNormalizer,
			ILogger<GenreRepository> logger
		)
		: base(context, localizer, lookupNormalizer, logger)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] IModelRepository
		/// <inheritdoc />
		public override async Task<Genre> CreateAsync(Genre genre)
		{
			return await base.CreateAsync(genre);
		}

		/// <inheritdoc />
		public override async Task<Genre> UpdateAsync(Genre genre)
		{
			return await base.UpdateAsync(genre);
		}

		/// <inheritdoc />
		public override async Task DeleteAsync(long genreId)
		{
			await base.DeleteAsync(genreId);
		}

		/// <inheritdoc />
		public override async Task<Genre> GetAsync(long genreId)
		{
			return await base.GetAsync(genreId);
		}

		/// <inheritdoc />
		public override async Task<IPage<Genre>> GetAllAsync(GenreFilter genreFilter = null)
		{
			return await base.GetAllAsync(genreFilter);
		}

		/// <inheritdoc />
		public override async Task<bool> ExistsAsync(long genreId)
		{
			return await base.ExistsAsync(genreId);
		}
		#endregion

		#region [Methods] IGenreRepository
		#endregion

		#region [Methods] Model
		/// <inheritdoc />
		protected override void NormalizeModel(Genre sourceGenre)
		{
			sourceGenre.NormalizedName = this.LookupNormalizer.NormalizeName(sourceGenre.Name ?? string.Empty);
		}

		/// <inheritdoc />
		protected override void ValidateModel(Genre sourceGenre)
		{
			var errorMessages = new List<string>();

			// Required fields
			if (string.IsNullOrWhiteSpace(sourceGenre.Name))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(genre => genre.Name));
			}

			// Duplicate fields
			if (this.Models.Any(genre => genre.Id != sourceGenre.Id && genre.NormalizedName.Equals(sourceGenre.NormalizedName)))
			{
				errorMessages.Add(this.GetModelHasDuplicateFieldMessage(genre => genre.Name));
			}

			if (errorMessages.Count > 0)
			{
				throw new MementoException(errorMessages, MementoExceptionType.BadRequest);
			}
		}

		/// <inheritdoc />
		protected override void UpdateModel(Genre sourceGenre, Genre targetGenre)
		{
			// Genre
			targetGenre.Name = sourceGenre.Name;
			targetGenre.NormalizedName = sourceGenre.NormalizedName;

			// Model
			targetGenre.CreatedAt = sourceGenre.CreatedAt;
			targetGenre.CreatedBy = sourceGenre.CreatedBy;
			targetGenre.UpdatedAt = sourceGenre.UpdatedAt;
			targetGenre.UpdatedBy = sourceGenre.UpdatedBy;
		}

		/// <inheritdoc />
		protected override void UpdateModelRelations(Genre sourceGenre, Genre targetGenre)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] Queryable
		/// <inheritdoc />
		protected override IQueryable<Genre> GetCountQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> GetSimpleQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> GetDetailedQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> FilterQueryable(IQueryable<Genre> genreQueryable, GenreFilter genreFilter)
		{
			// Apply the filter
			if (string.IsNullOrWhiteSpace(genreFilter.Name) == false)
			{
				var name = this.LookupNormalizer.NormalizeName(genreFilter.Name);

				genreQueryable = genreQueryable.Where(genre => EF.Functions.Like(genre.Name, $"%{name}%"));
			}

			// Apply the order
			switch (genreFilter.OrderBy)
			{
				case GenreFilterOrderBy.Id:
				{
					genreQueryable = OrderQueryable(genreQueryable, genreFilter, genre => genre.Id);
					break;
				}

				case GenreFilterOrderBy.Name:
				{
					genreQueryable = OrderQueryable(genreQueryable, genreFilter, genre => genre.Name);
					break;
				}

				case GenreFilterOrderBy.CreatedAt:
				{
					genreQueryable = OrderQueryable(genreQueryable, genreFilter, genre => genre.CreatedAt);
					break;
				}

				case GenreFilterOrderBy.UpdatedAt:
				{
					genreQueryable = OrderQueryable(genreQueryable, genreFilter, genre => genre.UpdatedAt);
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException(nameof(genreFilter.OrderBy));
				}
			}

			return genreQueryable;
		}

		/// <summary>
		/// Returns an ordered queryable according to the filters OrderDirection and expressions Property.
		/// </summary>
		///
		/// <typeparam name="TProperty">The property's type.</typeparam>
		///
		/// <param name="genreQueryable">The genre queryable.</param>
		/// <param name="genreFilter">The genre filter.</param>
		/// <param name="genreExpression">The genre expression</param>
		private static IQueryable<Genre> OrderQueryable<TProperty>(IQueryable<Genre> genreQueryable, GenreFilter genreFilter, Expression<Func<Genre, TProperty>> genreExpression)
		{
			switch (genreFilter.OrderDirection)
			{
				case GenreFilterOrderDirection.Ascending:
				{
					return genreQueryable.OrderBy(genreExpression);
				}
				case GenreFilterOrderDirection.Descending:
				{
					return genreQueryable.OrderByDescending(genreExpression);
				}
				default:
				{
					throw new ArgumentOutOfRangeException(nameof(genreFilter.OrderDirection));
				}
			}
		}
		#endregion

		#region [Methods] Messages
		/// <inheritdoc />
		protected override string GetModelDoesNotExistMessage()
		{
			// Get the name
			var name = this.Localizer.GetString(SharedResources.GENRE);

			return this.Localizer.GetString(SharedResources.ERROR_NOT_FOUND, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasDuplicateFieldMessage<TProperty>(Expression<Func<Genre, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_DUPLICATE_FIELD, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasInvalidFieldMessage<TProperty>(Expression<Func<Genre, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_INVALID_FIELD, name);
		}
		#endregion
	}
}