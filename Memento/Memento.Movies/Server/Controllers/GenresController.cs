using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Models.Genres;
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
	/// Implements the API controller for the genre model.
	/// </summary>
	///
	/// <seealso cref="BaseApiController" />
	[ApiController]
	[Route(Routes.GenreRoutes.Root)]
	public sealed class GenresController : BaseApiController
	{
		#region [Constants]
		/// <summary>
		/// The message when the 'CreateAsync' method is invoked successfully.
		/// </summary>
		private const string CREATE_SUCCESFULL = "The Genre was created successfully.";

		/// <summary>
		/// The message when the 'UpdateAsync' method is invoked successfully.
		/// </summary>
		private const string UPDATE_SUCCESFULL = "The Genre was updated successfully.";

		/// <summary>
		/// The message when the 'DeleteAsync' method is invoked successfully.
		/// </summary>
		private const string DELETE_SUCCESFULL = "The Genre was deleted successfully.";

		/// <summary>
		/// The message when the 'GetAsync' method is invoked successfully.
		/// </summary>
		private const string GET_SUCCESSFULL = "The Genre was queried successfully.";

		/// <summary>
		/// The message when the 'GetAllAsync' method is invoked successfully.
		/// </summary>
		private const string GET_ALL_SUCCESSFULL = "The Genres were queried successfully.";
		#endregion

		#region [Properties]
		/// <summary>
		/// The 'Genre' repository.
		/// </summary>
		private readonly IGenreRepository Repository;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="GenresController"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="mapper">The mapper.</param>
		public GenresController
		(
			IGenreRepository repository,
			ILogger<GenresController> logger,
			IMapper mapper
		)
		: base(logger, mapper)
		{
			this.Repository = repository;
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Creates a 'Genre' in the backend.
		/// </summary>
		/// 
		/// <param name="contract">The contract.</param>
		[HttpPost]
		public async Task<ActionResult<MementoResponse<GenreDetailContract>>> CreateAsync([FromBody] GenreCreateContract contract)
		{
			// Map the genre
			var genre = this.Mapper.Map<Genre>(contract);

			// Create the genre
			var createdGenre = await this.Repository.CreateAsync(genre);

			// Build the response
			var response = new MementoResponse<GenreDetailContract>(true, CREATE_SUCCESFULL, this.Mapper.Map<GenreDetailContract>(contract));

			return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{createdGenre.Id}"), response);
		}

		/// <summary>
		/// Updates a 'Genre' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		/// <param name="contract">The contract.</param>
		[HttpPut("{genreId:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] string contract)
		{
			// Map the genre
			var genre = this.Mapper.Map<Genre>(contract);
			genre.Id = id;

			// Update the genre
			await this.Repository.UpdateAsync(genre);

			// Build the response
			var response = new MementoResponse(true, UPDATE_SUCCESFULL);

			return this.Ok(response);
		}

		/// <summary>
		/// Deletes a 'Genre' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpDelete("{genreId:long}")]
		public async Task<ActionResult<MementoResponse>> DeleteAsync([FromRoute] long id)
		{
			// Delete the genre
			await this.Repository.DeleteAsync(id);

			// Build the response
			var response = new MementoResponse(true, DELETE_SUCCESFULL);

			return this.Ok(response);
		}

		/// <summary>
		/// Gets a 'Genre' from the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpGet("{genreId:long}")]
		public async Task<ActionResult<MementoResponse<GenreDetailContract>>> GetAsync([FromRoute] long id)
		{
			// Get the genres
			var genre = await this.Repository.GetAsync(id);
			var genreContract = this.Mapper.Map<GenreDetailContract>(genre);

			// Build the response
			var response = new MementoResponse<GenreDetailContract>(true, GET_SUCCESSFULL, genreContract);

			return this.Ok(response);
		}

		/// <summary>
		/// Gets 'Genres' from the backend according to the filter.
		/// </summary>
		/// 
		/// <param name="filter">The filter.</param>
		[HttpGet]
		public async Task<ActionResult<MementoResponse<Page<GenreListContract>>>> GetAsync([FromQuery] GenreFilter filter)
		{
			// Get the genres
			var genres = await this.Repository.GetAllAsync(filter);
			var genreContracts =  this.Mapper.Map<IEnumerable<GenreListContract>>(genres);

			// Build the genre page
			var genrePage = Page<GenreListContract>.CreateUnmodified
			(
				genreContracts,
				genres.TotalCount,
				genres.PageNumber,
				genres.PageSize,
				genres.OrderBy,
				genres.OrderDirection
			);

			// Build the response
			var response = new MementoResponse<Page<GenreListContract>>(true, GET_ALL_SUCCESSFULL, genrePage);

			return this.Ok(response);
		}
		#endregion
	}
}