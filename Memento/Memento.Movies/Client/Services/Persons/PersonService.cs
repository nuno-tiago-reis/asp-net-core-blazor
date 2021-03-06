﻿using Memento.Movies.Shared.Models.Movies.Contracts.Persons;
using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Responses;
using Memento.Shared.Services.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Persons
{
	/// <summary>
	/// Implements the interface for an API person service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public sealed class PersonService : IPersonService
	{
		#region [Constants]
		/// <summary>
		/// The api url.
		/// </summary>
		private const string API_URL = "/api/persons/";
		#endregion

		#region [Properties]
		/// <summary>
		/// The http service.
		/// </summary>
		private readonly IHttpService HttpService;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonService"/> class.
		/// </summary>
		/// 
		/// <param name="httpService">The http service.</param>
		public PersonService(IHttpService httpService)
		{
			this.HttpService = httpService;
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public async Task<MementoResponse<PersonDetailContract>> CreateAsync(PersonFormContract person)
		{
			// Invoke the API
			var response = await this.HttpService.PostAsync<PersonFormContract, PersonDetailContract>($"{API_URL}", person);
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
		public async Task<MementoResponse> UpdateAsync(long personId, PersonFormContract person)
		{
			// Invoke the API
			var response = await this.HttpService.PutAsync($"{API_URL}{personId}", person);
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
		public async Task<MementoResponse> DeleteAsync(long personId)
		{
			// Invoke the API
			var response = await this.HttpService.DeleteAsync($"{API_URL}{personId}");
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
		public async Task<MementoResponse<PersonDetailContract>> GetAsync(long personId)
		{
			// Invoke the API
			var response = await this.HttpService.GetAsync<PersonDetailContract>($"{API_URL}{personId}");
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
		public async Task<MementoResponse<Page<PersonListContract>>> GetAllAsync(PersonFilter personFilter = null)
		{
			var parameters = new Dictionary<string, string>();

			// Build the parameters
			if (personFilter != null)
			{
				// Populate the filter parameters
				if (string.IsNullOrWhiteSpace(personFilter.Name) == false)
				{
					parameters.Add(nameof(personFilter.Name), personFilter.Name);
				}
				if (string.IsNullOrWhiteSpace(personFilter.Biography) == false)
				{
					parameters.Add(nameof(personFilter.Biography), personFilter.Biography);
				}
				if (personFilter.BornAfter != null)
				{
					parameters.Add(nameof(personFilter.BornAfter), personFilter.BornAfter.Value.ToUtcString());
				}
				if (personFilter.BornBefore != null)
				{
					parameters.Add(nameof(personFilter.BornBefore), personFilter.BornBefore.Value.ToUtcString());
				}

				// Populate the pagination parameters
				parameters.Add(nameof(personFilter.PageNumber), personFilter.PageNumber.ToString());
				parameters.Add(nameof(personFilter.PageSize), personFilter.PageSize.ToString());
				parameters.Add(nameof(personFilter.OrderBy), personFilter.OrderBy.ToString());
				parameters.Add(nameof(personFilter.OrderDirection), personFilter.OrderDirection.ToString());
			}

			// Invoke the API
			var response = await this.HttpService.GetAsync<Page<PersonListContract>>($"{API_URL}", parameters);
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