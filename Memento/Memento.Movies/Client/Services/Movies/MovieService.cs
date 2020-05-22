using Memento.Movies.Shared.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Extensions;
using Memento.Shared.Pagination;
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
		public async Task<MovieDetailContract> CreateAsync(MovieCreateContract movie)
		{
			// Invoke the API
			var response = await this.HttpService.Post<MovieCreateContract, MovieDetailContract>($"{API_URL}", movie);
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
		public async Task UpdateAsync(long movieId, MovieUpdateContract movie)
		{
			// Invoke the API
			var response = await this.HttpService.Put($"{API_URL}/{movieId}", movie);
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
		public async Task DeleteMovie(long movieId)
		{
			// Invoke the API
			var response = await this.HttpService.Delete($"{API_URL}/{movieId}");
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
		public async Task<MovieDetailContract> GetAsync(long movieId)
		{
			// Invoke the API
			var response = await this.HttpService.Get<MovieDetailContract>($"{API_URL}/{movieId}");
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
		public async Task<Page<MovieListContract>> GetAllAsync(MovieFilter movieFilter = null)
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
			var response = await this.HttpService.Get<Page<MovieListContract>>($"{API_URL}", parameters);
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