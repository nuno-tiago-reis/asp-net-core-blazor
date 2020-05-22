using Memento.Shared.Models;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Database.Models.Genres
{
	/// <summary>
	/// Implements the interface for a 'Genre' repository.
	/// Provides methods to interact with the genres (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Genre" />
	/// <seealso cref="GenreFilter" />
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class GenreRepository : ModelRepository<Genre, GenreFilter, GenreFilterOrderBy, FilterOrderDirection>, IGenreRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="GenreRepository{Genre, GenreFilter, GenreFilterOrderBy, FilterOrderDirection}"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="serviceProvider">The services provider.</param>
		/// <param name="logger">The logger.</param>
		public GenreRepository
		(
			MovieContext context,
			ILookupNormalizer lookupNormalizer,
			IServiceProvider serviceProvider,
			ILogger<GenreRepository> logger
		)
		: base(context, lookupNormalizer, serviceProvider, logger)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] IModelRepository
		/// <inheritdoc />
		public async override Task<Genre> CreateAsync(Genre genre)
		{
			return await base.CreateAsync(genre);
		}

		/// <inheritdoc />
		public async override Task<Genre> UpdateAsync(Genre genre)
		{
			return await base.UpdateAsync(genre);
		}

		/// <inheritdoc />
		public async override Task DeleteAsync(long genreId)
		{
			await base.DeleteAsync(genreId);
		}

		/// <inheritdoc />
		public async override Task<Genre> GetAsync(long genreId)
		{
			return await base.GetAsync(genreId);
		}

		/// <inheritdoc />
		public async override Task<IPage<Genre>> GetAllAsync(GenreFilter genreFilter = null)
		{
			return await base.GetAllAsync(genreFilter);
		}

		/// <inheritdoc />
		public async override Task<bool> ExistsAsync(long genreId)
		{
			return await base.ExistsAsync(genreId);
		}
		#endregion

		#region [Methods] IGenreRepository
		#endregion

		#region [Methods] Utility
		/// <inheritdoc />
		protected override void NormalizeModel(Genre genre)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void ValidateModel(Genre genre)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void UpdateModel(Genre sourceGenre, Genre targetGenre)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> GetCountQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> GetSimpleQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Genre> GetDetailedQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override void FilterQueryable(IQueryable<Genre> genreQueryable, GenreFilter genreFilter)
		{
			// Nothing to do here.
		}
		#endregion
	}
}