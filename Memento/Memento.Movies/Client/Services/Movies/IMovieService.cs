using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
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
		Task<MementoResponse<MovieDetailContract>> CreateAsync(MovieFormContract movie);

		/// <summary>
		/// Invokes the API to update the given 'Movie'.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		/// <param name="movie">The movie.</param>
		Task<MementoResponse> UpdateAsync(long movieId, MovieFormContract movie);

		/// <summary>
		/// Invokes the API to delete the 'Movie' with the given identifier.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		Task<MementoResponse> DeleteAsync(long movieId);

		/// <summary>
		/// Invokes the API to get the 'Movie' with the given identifier.
		/// </summary>
		/// 
		/// <param name="movieId">The movie identifier.</param>
		Task<MementoResponse<MovieDetailContract>> GetAsync(long movieId);

		/// <summary>
		/// Invokes the API to get the 'Movies' according to the given filter.
		/// </summary>
		/// 
		/// <param name="movieFilter">The movie filter.</param>
		Task<MementoResponse<Page<MovieListContract>>> GetAllAsync(MovieFilter movieFilter = null);
		#endregion
	}
}