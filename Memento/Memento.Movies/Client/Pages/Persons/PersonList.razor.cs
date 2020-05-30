using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonList' component.
	/// </summary>
	/// 
	/// <seealso cref="MementoComponent{}"/>
	[Route(Routes.PersonRoutes.Root)]
	public sealed partial class PersonList : MementoComponent<PersonList>
	{
		#region [Properties] Constants
		/// <summary>
		/// The initial page number.
		/// </summary>
		private const int INITIAL_PAGE_NUMBER = 1;

		/// <summary>
		/// The initial page size.
		/// </summary>
		private const int INITIAL_PAGE_SIZE = 6;
		#endregion

		#region [Properties] Parameters
		/// <summary>
		/// The persons.
		/// </summary>
		[Parameter]
		public Page<PersonListContract> Persons { get; set; }

		/// <summary>
		/// The filter.
		/// </summary>
		[Parameter]
		public PersonFilter Filter { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person service.
		/// </summary>
		[Inject]
		public IPersonService PersonService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			// Initialize the filter
			this.Filter = new PersonFilter
			{
				PageNumber = INITIAL_PAGE_NUMBER,
				PageSize = INITIAL_PAGE_SIZE,
				OrderBy = PersonFilterOrderBy.Name,
				OrderDirection = PersonFilterOrderDirection.Ascending
			};

			// Initalize the movies
			await this.GetPersonsAsync();
		}
		#endregion

		#region [Methods] Search
		/// <summary>
		/// Gets the movies from the API using the filter.
		/// </summary>
		private async Task GetPersonsAsync()
		{
			// Invoke the API
			var response = await this.PersonService.GetAllAsync(this.Filter);
			if (response.Success)
			{
				// Update the movies
				this.Persons = response.Data;

				// Update the filter
				this.Filter = this.Mapper.Map<PersonFilter>(response.Data);

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the movies
				this.Persons = null;

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_LOADING));
			}
		}
		#endregion

		#region [Methods] Paging
		/// <summary>
		/// Callback that is invoked when the page number changes.
		/// </summary>
		///
		/// <param name="pageNumber">The page number.</param>
		private async Task OnPageNumberChangeAsync(int pageNumber)
		{
			// Update the filter
			this.Filter.PageNumber = pageNumber;
			this.Filter.PageSize = this.Filter.PageSize;

			// Update the movies
			await this.GetPersonsAsync();
		}

		/// <summary>
		/// Callback that is invoked when the page size changes.
		/// </summary>
		///
		/// <param name="pageSize">The page size.</param>
		private async Task OnPageSizeChangeAsync(int pageSize)
		{
			// Update the filter
			this.Filter.PageNumber = 1;
			this.Filter.PageSize = pageSize;

			// Update the movies
			await this.GetPersonsAsync();
		}
		#endregion

		#region [Methods] Ordering
		/// <summary>
		/// Invoked when the order by changes.
		/// </summary>
		///
		/// <param name="orderBy">The order by.</param>
		private async Task OnOrderByChangeAsync(PersonFilterOrderBy orderBy)
		{
			// Update the filter
			this.Filter.OrderBy = orderBy;

			// Update the movies
			await this.GetPersonsAsync();
		}

		/// <summary>
		/// Invoked when the order direction changes.
		/// </summary>
		///
		/// <param name="orderDirection">The order direction.</param>
		private async Task OnOrderDirectionChangeAsync(PersonFilterOrderDirection orderDirection)
		{
			// Update the filter
			this.Filter.OrderDirection = orderDirection;

			// Update the movies
			await this.GetPersonsAsync();
		}
		#endregion
	}
}