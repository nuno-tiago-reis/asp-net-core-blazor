﻿using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Models.Genres;
using Memento.Shared.Pagination;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Genres
{
	/// <summary>
	/// Defines an interface for an API genre service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public interface IGenreService
	{
		#region [Methods]
		/// <summary>
		/// Invokes the API to create the given 'Genre'.
		/// </summary>
		/// 
		/// <param name="genre">The genre.</param>
		Task<GenreDetailContract> CreateAsync(GenreCreateContract genre);

		/// <summary>
		/// Invokes the API to update the given 'Genre'.
		/// </summary>
		/// 
		/// <param name="genreId">The genre identifier.</param>
		/// <param name="genre">The genre.</param>
		Task UpdateAsync(long genreId, GenreUpdateContract genre);

		/// <summary>
		/// Invokes the API to delete the 'Genre' with the given identifier.
		/// </summary>
		/// 
		/// <param name="genreId">The genre identifier.</param>
		Task DeleteGenre(long genreId);

		/// <summary>
		/// Invokes the API to get the 'Genre' with the given identifier.
		/// </summary>
		/// 
		/// <param name="genreId">The genre identifier.</param>
		Task<GenreDetailContract> GetAsync(long genreId);

		/// <summary>
		/// Invokes the API to get the 'Genres' according to the given filter.
		/// </summary>
		/// 
		/// <param name="genreFilter">The genre filter.</param>
		Task<Page<GenreListContract>> GetAllAsync(GenreFilter genreFilter = null);
		#endregion
	}
}