using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Controllers;
using Memento.Shared.Exceptions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Memento.Movies.Server.Controllers.Movies
{
	/// <summary>
	/// Implements the API controller for the movie model.
	/// </summary>
	///
	/// <seealso cref="MementoApiController" />
	[ApiController]
	[Authorize]
	[Route(Routes.MovieRoutes.ROOT)]
	public sealed class MoviesController : MementoApiController
	{
		#region [Properties]
		/// <summary>
		/// The 'Movie' repository.
		/// </summary>
		private readonly IMovieRepository Repository;

		/// <summary>
		/// The 'Storage' service.
		/// </summary>
		private readonly IStorageService Storage;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MoviesController"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="mapper">The mapper.</param>
		/// <param name="localizer">The localizer.</param>
		/// <param name="storage">The storage.</param>
		public MoviesController
		(
			IMovieRepository repository,
			ILogger<MoviesController> logger,
			IMapper mapper,
			ILocalizerService localizer,
			IStorageService storage
		)
		: base(logger, mapper, localizer)
		{
			this.Repository = repository;
			this.Storage = storage;
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Creates a 'Movie' in the backend.
		/// </summary>
		///
		/// <param name="contract">The contract.</param>
		[HttpPost]
		public async Task<ActionResult<MementoResponse<MovieDetailContract>>> CreateAsync([FromBody] MovieFormContract contract)
		{
			// Check if there's a picture in the contract
			if (contract.Picture == null)
			{
				// Get the field name
				var name = this.Localizer.GetString(SharedResources.MOVIE_PICTURE);

				// Create the message with the given context
				var message =  this.Localizer.GetString(SharedResources.ERROR_INVALID_FIELD, name);

				// Throw an exception due to the missing picture
				throw new MementoException(message, MementoExceptionType.BadRequest);
			}

			// Map the movie
			var movie = this.Mapper.Map<Movie>(contract);

			// Create the picture in the storage
			movie.PictureUrl = await this.Storage.CreateAsync(contract.Picture.FileBase64, contract.Picture.FileName);

			// Create the movie
			var createdMovie = await this.Repository.CreateAsync(movie);

			// Build the response
			return this.BuildCreateResponse<Movie, MovieDetailContract>(createdMovie);
		}

		/// <summary>
		/// Updates a 'Movie' in the backend.
		/// </summary>
		///
		/// <param name="id">The identifier.</param>
		/// <param name="contract">The contract.</param>
		[HttpPut("{id:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] MovieFormContract contract)
		{
			// Map the movie
			var movie = this.Mapper.Map<Movie>(contract);
			movie.Id = id;

			// Check if there's a picture in the contract
			if (contract.Picture != null)
			{
				// Create the picture in the storage
				movie.PictureUrl = await this.Storage.CreateAsync(contract.Picture.FileBase64, contract.Picture.FileName);
			}
			else
			{
				movie.PictureUrl = (await this.Repository.GetAsync(id)).PictureUrl;
			}

			// Update the movie
			await this.Repository.UpdateAsync(movie);

			// Build the response
			return this.BuildUpdateResponse();
		}

		/// <summary>
		/// Deletes a 'Movie' in the backend.
		/// </summary>
		///
		/// <param name="id">The identifier.</param>
		[HttpDelete("{id:long}")]
		public async Task<ActionResult<MementoResponse>> DeleteAsync([FromRoute] long id)
		{
			// Delete the movie
			await this.Repository.DeleteAsync(id);

			// Build the response
			return this.BuildDeleteResponse();
		}

		/// <summary>
		/// Gets a 'Movie' from the backend.
		/// </summary>
		///
		/// <param name="id">The identifier.</param>
		[HttpGet("{id:long}")]
		public async Task<ActionResult<MementoResponse<MovieDetailContract>>> GetAsync([FromRoute] long id)
		{
			// Get the movies
			var movie = await this.Repository.GetAsync(id);

			// Build the response
			return this.BuildGetResponse<Movie, MovieDetailContract>(movie);
		}

		/// <summary>
		/// Gets 'Movies' from the backend according to the filter.
		/// </summary>
		/// 
		/// <param name="filter">The filter.</param>
		[HttpGet]
		public async Task<ActionResult<MementoResponse<IPage<MovieListContract>>>> GetAsync([FromQuery] MovieFilter filter)
		{
			// Get the movies
			var movies = await this.Repository.GetAllAsync(filter);

			// Build the response
			return this.BuildGetAllResponse<Movie, MovieListContract>(movies);
		}

		/// <summary>
		/// Gets 'Movies' from the backend that are in theaters
		/// </summary>
		[AllowAnonymous]
		[HttpGet(Routes.MovieRoutes.IN_THEATERS)]
		public async Task<ActionResult<MementoResponse<IPage<MovieListContract>>>> GetInTheatersAsync()
		{
			// Build the 'InTheaters' filter
			var filter = new MovieFilter
			{
				InTheaters = MovieFilterInTheaters.Checked,
				ReleasedBefore = DateTime.Today,
				PageNumber = 1,
				PageSize = 3,
				OrderBy = MovieFilterOrderBy.ReleaseDate,
				OrderDirection = MovieFilterOrderDirection.Descending
			};

			// Get the movies
			var movies = await this.Repository.GetAllAsync(filter);

			// Build the response
			return this.BuildGetAllResponse<Movie, MovieListContract>(movies);
		}

		/// <summary>
		/// Gets 'Movies' from the backend that are upcoming releases.
		/// </summary>
		[AllowAnonymous]
		[HttpGet(Routes.MovieRoutes.UPCOMING_RELEASES)]
		public async Task<ActionResult<MementoResponse<IPage<MovieListContract>>>> GetUpcomingReleasesAsync()
		{
			// Build the 'UpcomingReleases' filter
			var filter = new MovieFilter
			{
				InTheaters = MovieFilterInTheaters.Unchecked,
				ReleasedAfter = DateTime.Today,
				PageNumber = 1,
				PageSize = 3,
				OrderBy = MovieFilterOrderBy.ReleaseDate,
				OrderDirection = MovieFilterOrderDirection.Ascending
			};

			// Get the movies
			var movies = await this.Repository.GetAllAsync(filter);

			// Build the response
			return this.BuildGetAllResponse<Movie, MovieListContract>(movies);
		}
		#endregion

		#region [Methods] Messages
		/// <inheritdoc />
		protected override string BuildCreateSuccessfulMessage()
		{
			// Build the message
			var message = this.Localizer.GetString(SharedResources.CONTROLLER_CREATE_SUCCESSFUL);

			return message;
		}

		/// <inheritdoc />
		protected override string BuildUpdateSuccessfulMessage()
		{
			// Build the message
			var message = this.Localizer.GetString(SharedResources.CONTROLLER_UPDATE_SUCCESSFUL);

			return message;
		}

		/// <inheritdoc />
		protected override string BuildDeleteSuccessfulMessage()
		{
			// Build the message
			var message = this.Localizer.GetString(SharedResources.CONTROLLER_DELETE_SUCCESSFUL);

			return message;
		}

		/// <inheritdoc />
		protected override string BuildGetSuccessfulMessage()
		{
			// Build the message
			var message = this.Localizer.GetString(SharedResources.CONTROLLER_GET_SUCCESSFUL);

			return message;
		}

		/// <inheritdoc />
		protected override string BuildGetAllSuccessfulMessage()
		{
			// Build the message
			var message = this.Localizer.GetString(SharedResources.CONTROLLER_GET_ALL_SUCCESSFUL);

			return message;
		}
		#endregion
	}
}