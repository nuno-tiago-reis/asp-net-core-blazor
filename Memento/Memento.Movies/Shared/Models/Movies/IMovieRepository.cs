using Memento.Shared.Models;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Defines an interface for a 'Movie' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Movie" />
	/// <seealso cref="MovieFilter" />
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public interface IMovieRepository : IModelRepository<Movie, MovieFilter, MovieFilterOrderBy, FilterOrderDirection>
	{
		#region [Methods] IMovieRepository
		#endregion
	}
}