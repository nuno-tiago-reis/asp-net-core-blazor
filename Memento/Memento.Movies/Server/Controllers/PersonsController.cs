using AutoMapper;
using Memento.Movies.Server.Shared.Routes;
using Memento.Movies.Shared.Contracts.Persons;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Controllers;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Memento.Movies.Server.Controllers
{
	/// <summary>
	/// Implements the API controller for the person model.
	/// </summary>
	///
	/// <seealso cref="BaseApiController" />
	[ApiController]
	[Route(Routes.PersonRoutes.Root)]
	public sealed class PersonsController : BaseApiController
	{
		#region [Constants]
		/// <summary>
		/// The message when the 'CreateAsync' method is invoked successfully.
		/// </summary>
		private const string CREATE_SUCCESFULL = "The Person was created successfully.";

		/// <summary>
		/// The message when the 'UpdateAsync' method is invoked successfully.
		/// </summary>
		private const string UPDATE_SUCCESFULL = "The Person was updated successfully.";

		/// <summary>
		/// The message when the 'DeleteAsync' method is invoked successfully.
		/// </summary>
		private const string DELETE_SUCCESFULL = "The Person was deleted successfully.";

		/// <summary>
		/// The message when the 'GetAsync' method is invoked successfully.
		/// </summary>
		private const string GET_SUCCESSFULL = "The Person was queried successfully.";

		/// <summary>
		/// The message when the 'GetAllAsync' method is invoked successfully.
		/// </summary>
		private const string GET_ALL_SUCCESSFULL = "The Persons were queried successfully.";
		#endregion

		#region [Properties]
		/// <summary>
		/// The 'Person' repository.
		/// </summary>
		private readonly IPersonRepository Repository;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonsController"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="mapper">The mapper.</param>
		public PersonsController
		(
			IPersonRepository repository,
			ILogger<PersonsController> logger,
			IMapper mapper
		)
		: base(logger, mapper)
		{
			this.Repository = repository;
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Creates a 'Person' in the backend.
		/// </summary>
		/// 
		/// <param name="contract">The contract.</param>
		[HttpPost]
		public async Task<ActionResult<MementoResponse<PersonDetailContract>>> CreateAsync([FromBody] PersonFormContract contract)
		{
			// Map the person
			var person = this.Mapper.Map<Person>(contract);

			// Create the person
			var createdPerson = await this.Repository.CreateAsync(person);

			// Build the response
			var response = new MementoResponse<PersonDetailContract>(true, CREATE_SUCCESFULL, this.Mapper.Map<PersonDetailContract>(createdPerson));

			// Build the response header
			this.HttpContext.Response.AddMementoHeader();

			return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{createdPerson.Id}"), response);
		}

		/// <summary>
		/// Updates a 'Person' in the backend.
		/// </summary>
		/// 
		/// <param name="id">The identifer.</param>
		/// <param name="contract">The contract.</param>
		[HttpPut("{id:long}")]
		public async Task<ActionResult<MementoResponse>> UpdateAsync([FromRoute] long id, [FromBody] PersonFormContract contract)
		{
			// Map the person
			var person = this.Mapper.Map<Person>(contract);
			person.Id = id;

			// Update the person
			await this.Repository.UpdateAsync(person);

			// Build the response
			var response = new MementoResponse(true, UPDATE_SUCCESFULL);

			// Build the response header
			this.HttpContext.Response.AddMementoHeader();

			return this.Ok(response);
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
			var response = new MementoResponse(true, DELETE_SUCCESFULL);

			// Build the response header
			this.HttpContext.Response.AddMementoHeader();

			return this.Ok(response);
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
			var personContract = this.Mapper.Map<PersonDetailContract>(person);

			// Build the response
			var response = new MementoResponse<PersonDetailContract>(true, GET_SUCCESSFULL, personContract);

			// Build the response header
			this.HttpContext.Response.AddMementoHeader();

			return this.Ok(response);
		}

		/// <summary>
		/// Gets 'Persons' from the backend according to the filter.
		/// </summary>
		/// 
		/// <param name="filter">The filter.</param>
		[HttpGet]
		public async Task<ActionResult<MementoResponse<Page<PersonListContract>>>> GetAsync([FromQuery] PersonFilter filter)
		{
			// Get the persons
			var persons = await this.Repository.GetAllAsync(filter);
			var personContracts = this.Mapper.Map<Page<PersonListContract>>(persons);

			// Build the response
			var response = new MementoResponse<Page<PersonListContract>>(true, GET_ALL_SUCCESSFULL, personContracts);

			// Build the response header
			this.HttpContext.Response.AddMementoHeader();

			return this.Ok(response);
		}
		#endregion
	}
}