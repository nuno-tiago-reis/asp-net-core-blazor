using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Controllers;
using Memento.Shared.Exceptions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Memento.Movies.Server.Controllers
{
	/// <summary>
	/// Implements the API controller for the person model.
	/// </summary>
	///
	/// <seealso cref="MementoApiController" />
	[ApiController]
	[Route(Routes.PersonRoutes.Root)]
	public sealed class PersonsController : MementoApiController
	{
		#region [Properties]
		/// <summary>
		/// The 'Person' repository.
		/// </summary>
		private readonly IPersonRepository Repository;

		/// <summary>
		/// The 'Storage' service.
		/// </summary>
		private readonly IStorageService Storage;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonsController"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="mapper">The mapper.</param>
		/// <param name="localizer">The localizer.</param>
		/// <param name="storage">The storage.</param>
		public PersonsController
		(
			IPersonRepository repository,
			ILogger<PersonsController> logger,
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
		/// Creates a 'Person' in the backend.
		/// </summary>
		/// 
		/// <param name="contract">The contract.</param>
		/// <param name="file">The file.</param>
		[HttpPost]
		public async Task<ActionResult<MementoResponse<PersonDetailContract>>> CreateAsync([FromBody] PersonFormContract contract)
		{
			// Check if there's a picture in the contract
			if (contract.Picture == null)
			{
				// Get the field name
				var name = this.Localizer.GetString(SharedResources.PERSON_PICTURE);

				// Create the message with the given context
				var message = this.Localizer.GetString(SharedResources.ERROR_INVALID_FIELD, name);

				// Throw an exception due to the missing picture
				throw new MementoException(message, MementoExceptionType.BadRequest);
			}

			// Map the person
			var person = this.Mapper.Map<Person>(contract);

			// Create the picture in the storage
			person.PictureUrl = await this.Storage.CreateAsync(contract.Picture.FileBase64, contract.Picture.FileName);

			// Create the person
			var createdPerson = await this.Repository.CreateAsync(person);

			// Build the response
			return this.BuildCreateResponse<Person, PersonDetailContract>(createdPerson);
		}

		/// <summary>
		/// Updates a 'Person' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		/// <param name="contract">The contract.</param>
		/// <param name="file">The file.</param>
		[HttpPut("{id:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] PersonFormContract contract)
		{
			// Map the person
			var person = this.Mapper.Map<Person>(contract);
			person.Id = id;

			// Check if there's a picture in the contract
			if (contract.Picture != null)
			{
				// Create the picture in the storage
				person.PictureUrl = await this.Storage.CreateAsync(contract.Picture.FileBase64, contract.Picture.FileName);
			}
			else
			{
				person.PictureUrl = (await this.Repository.GetAsync(id)).PictureUrl;
			}

			// Update the person
			await this.Repository.UpdateAsync(person);

			// Build the response
			return this.BuildUpdateResponse<Person>();
		}

		/// <summary>
		/// Deletes a 'Person' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpDelete("{id:long}")]
		public async Task<ActionResult<MementoResponse>> DeleteAsync([FromRoute] long id)
		{
			// Delete the person
			await this.Repository.DeleteAsync(id);

			// Build the response
			return this.BuildDeleteResponse<Person>();
		}

		/// <summary>
		/// Gets a 'Person' from the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		[HttpGet("{id:long}")]
		public async Task<ActionResult<MementoResponse<PersonDetailContract>>> GetAsync([FromRoute] long id)
		{
			// Get the persons
			var person = await this.Repository.GetAsync(id);

			// Build the response
			return this.BuildGetResponse<Person, PersonDetailContract>(person);
		}

		/// <summary>
		/// Gets 'Persons' from the backend according to the filter.
		/// </summary>
		/// 
		/// <param name="filter">The filter.</param>
		[HttpGet]
		public async Task<ActionResult<MementoResponse<IPage<PersonListContract>>>> GetAsync([FromQuery] PersonFilter filter)
		{
			// Get the persons
			var persons = await this.Repository.GetAllAsync(filter);

			// Build the response
			return this.BuildGetAllResponse<Person, PersonListContract>(persons);
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