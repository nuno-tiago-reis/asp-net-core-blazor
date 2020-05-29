using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Genres
{
	/// <summary>
	/// Implements the interface for an API genre service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public sealed class GenreService : IGenreService
	{
		#region [Constants]
		/// <summary>
		/// The api url.
		/// </summary>
		private const string API_URL = "/api/genres/";
		#endregion

		#region [Properties]
		/// <summary>
		/// The http service.
		/// </summary>
		private readonly IHttpService HttpService;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="GenreService"/> class.
		/// </summary>
		/// 
		/// <param name="httpService">The http service.</param>
		public GenreService(IHttpService httpService)
		{
			this.HttpService = httpService;
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public async Task<MementoResponse<GenreDetailContract>> CreateAsync(GenreFormContract genre)
		{
			// Invoke the API
			var response = await this.HttpService.PostAsync<GenreFormContract, GenreDetailContract>($"{API_URL}", genre);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response;
			}
		}

		/// <inheritdoc />
		public async Task<MementoResponse> UpdateAsync(long genreId, GenreFormContract genre)
		{
			// Invoke the API
			var response = await this.HttpService.PutAsync($"{API_URL}{genreId}", genre);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response;
			}
		}

		/// <inheritdoc />
		public async Task<MementoResponse> DeleteAsync(long genreId)
		{
			// Invoke the API
			var response = await this.HttpService.DeleteAsync($"{API_URL}{genreId}");
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response;
			}
		}

		/// <inheritdoc />
		public async Task<MementoResponse<GenreDetailContract>> GetAsync(long genreId)
		{
			// Invoke the API
			var response = await this.HttpService.GetAsync<GenreDetailContract>($"{API_URL}{genreId}");
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response;
			}
		}

		/// <inheritdoc />
		public async Task<MementoResponse<Page<GenreListContract>>> GetAllAsync(GenreFilter genreFilter = null)
		{
			var parameters = new Dictionary<string, string>();

			// Build the parameters
			if (genreFilter != null)
			{
				// Populate the filter parameters
				if (string.IsNullOrWhiteSpace(genreFilter.Name) == false)
				{
					parameters.Add(nameof(genreFilter.Name), genreFilter.Name);
				}

				// Populate the pagination parameters
				parameters.Add(nameof(genreFilter.PageNumber), genreFilter.PageNumber.ToString());
				parameters.Add(nameof(genreFilter.PageSize), genreFilter.PageSize.ToString());
				parameters.Add(nameof(genreFilter.OrderBy), genreFilter.OrderBy.ToString());
				parameters.Add(nameof(genreFilter.OrderDirection), genreFilter.OrderDirection.ToString());
			}

			// Invoke the API
			var response = await this.HttpService.GetAsync<Page<GenreListContract>>($"{API_URL}", parameters);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response;
			}
		}
		#endregion
	}
}