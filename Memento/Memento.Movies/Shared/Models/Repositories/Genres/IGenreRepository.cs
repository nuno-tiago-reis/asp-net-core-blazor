using Memento.Shared.Models.Repositories;

namespace Memento.Movies.Shared.Models.Repositories.Genres
{
	/// <summary>
	/// Defines an interface for a 'Genre' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Genre" />
	/// <seealso cref="GenreFilter" />
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="GenreFilterOrderDirection" />
	public interface IGenreRepository : IModelRepository<Genre, GenreFilter, GenreFilterOrderBy, GenreFilterOrderDirection>
	{
		#region [Methods] IGenreRepository
		#endregion
	}
}