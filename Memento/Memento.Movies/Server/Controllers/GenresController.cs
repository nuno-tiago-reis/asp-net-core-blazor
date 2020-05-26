using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Shared.Controllers;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Memento.Movies.Server.Controllers
{
	/// <summary>
	/// Implements the API controller for the genre model.
	/// </summary>
	///
	/// <seealso cref="MementoApiController" />
	[ApiController]
	[Route(Routes.GenreRoutes.Root)]
	public sealed class GenresController : MementoApiController
	{
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
		/// <param name="localizer">The string localizer.</param>
		public GenresController
		(
			IGenreRepository repository,
			ILogger<GenresController> logger,
			IMapper mapper,
			ILocalizerService localizer
		)
		: base(logger, mapper, localizer)
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
		public async Task<ActionResult<MementoResponse<GenreDetailContract>>> CreateAsync([FromBody] GenreFormContract contract)
		{
			// Map the genre
			var genre = this.Mapper.Map<Genre>(contract);

			// Create the genre
			var createdGenre = await this.Repository.CreateAsync(genre);

			// Build the response
			return this.BuildCreateResponse<Genre, GenreDetailContract>(createdGenre);
		}

		/// <summary>
		/// Updates a 'Genre' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		/// <param name="contract">The contract.</param>
		[HttpPut("{id:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] GenreFormContract contract)
		{
			// Map the genre
			var genre = this.Mapper.Map<Genre>(contract);
			genre.Id = id;

			// Update the genre
			await this.Repository.UpdateAsync(genre);

			// Build the response
			return this.BuildUpdateResponse<Genre>();
		}

		/// <summary>
		/// Deletes a 'Genre' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpDelete("{id:long}")]
		public async Task<ActionResult<MementoResponse>> DeleteAsync([FromRoute] long id)
		{
			// Delete the genre
			await this.Repository.DeleteAsync(id);

			// Build the response
			return this.BuildDeleteResponse<Genre>();
		}

		/// <summary>
		/// Gets a 'Genre' from the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpGet("{id:long}")]
		public async Task<ActionResult<MementoResponse<GenreDetailContract>>> GetAsync([FromRoute] long id)
		{
			// Get the genres
			var genre = await this.Repository.GetAsync(id);

			// Build the response
			return this.BuildGetResponse<Genre, GenreDetailContract>(genre);
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

			// Build the response
			return this.BuildGetAllResponse<Genre, GenreListContract>(genres);
		}
		#endregion
	}
}