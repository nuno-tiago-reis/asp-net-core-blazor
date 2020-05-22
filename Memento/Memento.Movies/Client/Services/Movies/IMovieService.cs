using Memento.Movies.Shared.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Pagination;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Movies
{
	/// <summary>
	/// Defines an interface for an API movie service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public interface IMovieService
	{
		#region [Methods]
		/// <summary>
		/// Invokes the API to create the given 'Movie'.
		/// </summary>
		/// 
		/// <param name="movie">The movie.</param>
		Task<MovieDetailContract> CreateAsync(MovieCreateContract movie);

		/// <summary>
		/// Invokes the API to update the given 'Movie'.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		/// <param name="movie">The movie.</param>
		Task UpdateAsync(long movieId, MovieUpdateContract movie);

		/// <summary>
		/// Invokes the API to delete the 'Movie' with the given identifier.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		Task DeleteMovie(long movieId);

		/// <summary>
		/// Invokes the API to get the 'Movie' with the given identifier.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		Task<MovieDetailContract> GetAsync(long movieId);

		/// <summary>
		/// Invokes the API to get the 'Movies' according to the given filter.
		/// </summary>
		/// 
		/// <param name="movieFilter">The movie filter.</param>
		Task<Page<MovieListContract>> GetAllAsync(MovieFilter movieFilter = null);
		#endregion
	}
}