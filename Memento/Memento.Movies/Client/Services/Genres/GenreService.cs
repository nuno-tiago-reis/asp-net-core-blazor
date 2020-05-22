using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Models.Genres;
using Memento.Shared.Extensions;
using Memento.Shared.Pagination;
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
		public async Task<GenreDetailContract> CreateAsync(GenreCreateContract genre)
		{
			// Invoke the API
			var response = await this.HttpService.Post<GenreCreateContract, GenreDetailContract>($"{API_URL}", genre);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response.Data;
			}
		}

		/// <inheritdoc />
		public async Task UpdateAsync(long genreId, GenreUpdateContract genre)
		{
			// Invoke the API
			var response = await this.HttpService.Put($"{API_URL}/{genreId}", genre);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return;
			}
		}

		/// <inheritdoc />
		public async Task DeleteGenre(long genreId)
		{
			// Invoke the API
			var response = await this.HttpService.Delete($"{API_URL}/{genreId}");
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return;
			}
		}

		/// <inheritdoc />
		public async Task<GenreDetailContract> GetAsync(long genreId)
		{
			// Invoke the API
			var response = await this.HttpService.Get<GenreDetailContract>($"{API_URL}/{genreId}");
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response.Data;
			}
		}

		/// <inheritdoc />
		public async Task<Page<GenreListContract>> GetAllAsync(GenreFilter genreFilter = null)
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
			var response = await this.HttpService.Get<Page<GenreListContract>>($"{API_URL}", parameters);
			if (!response.Success)
			{
				throw new ApplicationException(string.Join(Environment.NewLine, response.Errors));
			}
			else
			{
				return response.Data;
			}
		}
		#endregion
	}
}