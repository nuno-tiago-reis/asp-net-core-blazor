using Memento.Shared.Models.Repositories;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Movies
{
	/// <summary>
	/// Defines an interface for a 'Movie' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Movie" />
	/// <seealso cref="MovieFilter" />
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="MovieFilterOrderDirection" />
	public interface IMovieRepository : IModelRepository<Movie, MovieFilter, MovieFilterOrderBy, MovieFilterOrderDirection>
	{
		#region [Methods] IMovieRepository
		#endregion
	}
}