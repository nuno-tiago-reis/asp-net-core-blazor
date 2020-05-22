using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Controlers;
using Memento.Shared.Controllers;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Server.Controller
{
	/// <summary>
	/// Implements the API controller for the movie model.
	/// </summary>
	///
	/// <seealso cref="BaseApiController" />
	[ApiController]
	[Route(Routes.MovieRoutes.Root)]
	public sealed class MoviesController : BaseApiController
	{
		#region [Constants]
		/// <summary>
		/// The message when the 'CreateAsync' method is invoked successfully.
		/// </summary>
		private const string CREATE_SUCCESFULL = "The Movie was created successfully.";

		/// <summary>
		/// The message when the 'UpdateAsync' method is invoked successfully.
		/// </summary>
		private const string UPDATE_SUCCESFULL = "The Movie was updated successfully.";

		/// <summary>
		/// The message when the 'DeleteAsync' method is invoked successfully.
		/// </summary>
		private const string DELETE_SUCCESFULL = "The Movie was deleted successfully.";

		/// <summary>
		/// The message when the 'GetAsync' method is invoked successfully.
		/// </summary>
		private const string GET_SUCCESSFULL = "The Movie was queried successfully.";

		/// <summary>
		/// The message when the 'GetAllAsync' method is invoked successfully.
		/// </summary>
		private const string GET_ALL_SUCCESSFULL = "The Movies were queried successfully.";
		#endregion

		#region [Properties]
		/// <summary>
		/// The 'Movie' repository.
		/// </summary>
		private readonly IMovieRepository Repository;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MoviesController"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="mapper">The mapper.</param>
		public MoviesController
		(
			IMovieRepository repository,
			ILogger<MoviesController> logger,
			IMapper mapper
		)
		: base(logger, mapper)
		{
			this.Repository = repository;
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Creates a 'Movie' in the backend.
		/// </summary>
		/// 
		/// <param name="contract">The contract.</param>
		[HttpPost]
		public async Task<ActionResult<MementoResponse<MovieDetailContract>>> CreateAsync([FromBody] MovieCreateContract contract)
		{
			// Map the movie
			var movie = this.Mapper.Map<Movie>(contract);

			// Create the movie
			var createdMovie = await this.Repository.CreateAsync(movie);

			// Build the response
			var response = new MementoResponse<MovieDetailContract>(true, CREATE_SUCCESFULL, this.Mapper.Map<MovieDetailContract>(contract));

			return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{createdMovie.Id}"), response);
		}

		/// <summary>
		/// Updates a 'Movie' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		/// <param name="contract">The contract.</param>
		[HttpPut("{movieId:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] string contract)
		{
			// Map the movie
			var movie = this.Mapper.Map<Movie>(contract);
			movie.Id = id;

			// Update the movie
			await this.Repository.UpdateAsync(movie);

			// Build the response
			var response = new MementoResponse(true, UPDATE_SUCCESFULL);

			return this.Ok(response);
		}

		/// <summary>
		/// Deletes a 'Movie' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpDelete("{movieId:long}")]
		public async Task<ActionResult<MementoResponse>> DeleteAsync([FromRoute] long id)
		{
			// Delete the movie
			await this.Repository.DeleteAsync(id);

			// Build the response
			var response = new MementoResponse(true, DELETE_SUCCESFULL);

			return this.Ok(response);
		}

		/// <summary>
		/// Gets a 'Movie' from the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpGet("{movieId:long}")]
		public async Task<ActionResult<MementoResponse<MovieDetailContract>>> GetAsync([FromRoute] long id)
		{
			// Get the movies
			var movie = await this.Repository.GetAsync(id);
			var movieContract = this.Mapper.Map<MovieDetailContract>(movie);

			// Build the response
			var response = new MementoResponse<MovieDetailContract>(true, GET_SUCCESSFULL, movieContract);

			return this.Ok(response);
		}

		/// <summary>
		/// Gets 'Movies' from the backend according to the filter.
		/// </summary>
		/// 
		/// <param name="filter">The filter.</param>
		[HttpGet]
		public async Task<ActionResult<MementoResponse<Page<MovieListContract>>>> GetAsync([FromQuery] MovieFilter filter)
		{
			// Get the movies
			var movies = await this.Repository.GetAllAsync(filter);
			var movieContracts = this.Mapper.Map<IEnumerable<MovieListContract>>(movies);

			// Build the movie page
			var moviePage = Page<MovieListContract>.CreateUnmodified
			(
				movieContracts,
				movies.TotalCount,
				movies.PageNumber,
				movies.PageSize,
				movies.OrderBy,
				movies.OrderDirection
			);

			// Build the response
			var response = new MementoResponse<Page<MovieListContract>>(true, GET_ALL_SUCCESSFULL, moviePage);

			return this.Ok(response);
		}
		#endregion
	}
}