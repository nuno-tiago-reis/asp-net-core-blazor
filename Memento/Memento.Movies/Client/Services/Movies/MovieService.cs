using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Movies
{
	/// <summary>
	/// Implements the interface for an API movie service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public sealed class MovieService : IMovieService
	{
		#region [Constants]
		/// <summary>
		/// The api url.
		/// </summary>
		private const string API_URL = "/api/movies/";
		#endregion

		#region [Properties]
		/// <summary>
		/// The http service.
		/// </summary>
		private readonly IHttpService HttpService;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieService"/> class.
		/// </summary>
		/// 
		/// <param name="httpService">The http service.</param>
		public MovieService(IHttpService httpService)
		{
			this.HttpService = httpService;
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public async Task<MementoResponse<MovieDetailContract>> CreateAsync(MovieFormContract movie)
		{
			// Invoke the API
			var response = await this.HttpService.PostAsync<MovieFormContract, MovieDetailContract>($"{API_URL}", movie);
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
		public async Task<MementoResponse> UpdateAsync(long movieId, MovieFormContract movie)
		{
			// Invoke the API
			var response = await this.HttpService.PutAsync($"{API_URL}{movieId}", movie);
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
		public async Task<MementoResponse> DeleteAsync(long movieId)
		{
			// Invoke the API
			var response = await this.HttpService.DeleteAsync($"{API_URL}{movieId}");
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
		public async Task<MementoResponse<MovieDetailContract>> GetAsync(long movieId)
		{
			// Invoke the API
			var response = await this.HttpService.GetAsync<MovieDetailContract>($"{API_URL}{movieId}");
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
		public async Task<MementoResponse<Page<MovieListContract>>> GetAllAsync(MovieFilter movieFilter = null)
		{
			var parameters = new Dictionary<string, string>();

			// Build the parameters
			if (movieFilter != null)
			{
				// Populate the filter parameters
				if (string.IsNullOrWhiteSpace(movieFilter.Name) == false)
				{
					parameters.Add(nameof(movieFilter.Name), movieFilter.Name);
				}
				if (string.IsNullOrWhiteSpace(movieFilter.Summary) == false)
				{
					parameters.Add(nameof(movieFilter.Summary), movieFilter.Summary);
				}
				if (movieFilter.ReleasedAfter != null)
				{
					parameters.Add(nameof(movieFilter.ReleasedAfter), movieFilter.ReleasedAfter.Value.ToUtcString());
				}
				if (movieFilter.ReleasedBefore != null)
				{
					parameters.Add(nameof(movieFilter.ReleasedBefore), movieFilter.ReleasedBefore.Value.ToUtcString());
				}
				if (movieFilter.InTheaters != null)
				{
					parameters.Add(nameof(movieFilter.InTheaters), movieFilter.InTheaters.ToString());
				}

				// Populate the pagination parameters
				parameters.Add(nameof(movieFilter.PageNumber), movieFilter.PageNumber.ToString());
				parameters.Add(nameof(movieFilter.PageSize), movieFilter.PageSize.ToString());
				parameters.Add(nameof(movieFilter.OrderBy), movieFilter.OrderBy.ToString());
				parameters.Add(nameof(movieFilter.OrderDirection), movieFilter.OrderDirection.ToString());
			}

			// Invoke the API
			var response = await this.HttpService.GetAsync<Page<MovieListContract>>($"{API_URL}", parameters);
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
		public async Task<MementoResponse<Page<MovieListContract>>> GetInTheatersAsync()
		{
			// Invoke the API
			var response = await this.HttpService.GetAsync<Page<MovieListContract>>($"{API_URL}in-theaters");
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
		public async Task<MementoResponse<Page<MovieListContract>>> GetUpcomingReleasesAsync()
		{
			// Invoke the API
			var response = await this.HttpService.GetAsync<Page<MovieListContract>>($"{API_URL}upcoming-releases");
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