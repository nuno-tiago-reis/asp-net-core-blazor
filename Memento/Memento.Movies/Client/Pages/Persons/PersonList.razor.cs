using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies.Contracts.Persons;
using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonList' component.
	/// </summary>
	/// 
	/// <seealso cref="MementoComponent{PersonList}"/>
	[Authorize]
	[Route(Routes.PersonRoutes.ROOT)]
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

		#region [Properties] Internal
		/// <summary>
		/// The persons.
		/// </summary>
		private Page<PersonListContract> Persons { get; set; }

		/// <summary>
		/// The filter.
		/// </summary>
		private PersonFilter Filter { get; set; }

		/// <summary>
		/// The breadcrumb header.
		/// </summary>
		private string BreadcrumbHeader { get; set; }

		/// <summary>
		/// The breadcrumb links.
		/// </summary>
		private List<BreadcrumbLink> BreadcrumbLinks { get; set; }

		/// <summary>
		/// The breadcrumb actions.
		/// </summary>
		private List<BreadcrumbAction> BreadcrumbActions { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person service.
		/// </summary>
		[Inject]
		private IPersonService PersonService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override async Task OnInitializedAsync()
		{
			// Build the filter
			this.BuildQueryFilter();

			// Get the movies
			await this.GetPersonsAsync();

			// Build the breadcrumb
			this.BuildBreadcrumb();
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

				// Update the paging
				this.Mapper.Map(response.Data, this.Filter);

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

			// Build the url
			var uri = this.NavigationManager.Uri.Split("?").First();
			var uriWithQuery = QueryHelpers.AddQueryString(uri, this.Filter.WriteToQuery());

			// Update the url
			this.NavigationManager.NavigateTo(uriWithQuery);
		}
		#endregion

		#region [Methods] Filtering
		/// <summary>
		/// Builds the filter from the query string.
		/// </summary>
		private void BuildQueryFilter()
		{
			// Get the query from the url
			var query = QueryHelpers.ParseQuery(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query);

			// Create the filter
			this.Filter = BuildDefaultFilter();

			// Parse the query
			this.Filter.ReadFromQuery(query);
		}

		/// <summary>
		/// Builds the default filter from the built-in constants.
		/// </summary>
		private static PersonFilter BuildDefaultFilter()
		{
			// Create the filter
			var filter = new PersonFilter
			{
				PageNumber = INITIAL_PAGE_NUMBER,
				PageSize = INITIAL_PAGE_SIZE,
				OrderBy = PersonFilterOrderBy.Name,
				OrderDirection = PersonFilterOrderDirection.Ascending
			};

			return filter;
		}

		/// <summary>
		/// Callback that is invoked when the filter is applied.
		/// </summary>
		///
		/// <param name="_">The filter.</param>
		private async Task OnFilterSearchAsync(PersonFilter _)
		{
			// Reset the paging
			this.Filter.PageNumber = 1;
			this.Filter.PageSize = this.Filter.PageSize;

			// Update the persons
			await this.GetPersonsAsync();
		}

		/// <summary>
		/// Callback that is invoked when the filter is reset.
		/// </summary>
		///
		/// <param name="_">The filter.</param>
		private async Task OnFilterResetAsync(PersonFilter _)
		{
			// Reset the filter
			this.Filter = BuildDefaultFilter();

			// Update the persons
			await this.GetPersonsAsync();
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
			// Update the paging
			this.Filter.PageNumber = pageNumber;
			this.Filter.PageSize = this.Filter.PageSize;

			// Update the persons
			await this.GetPersonsAsync();
		}

		/// <summary>
		/// Callback that is invoked when the page size changes.
		/// </summary>
		///
		/// <param name="pageSize">The page size.</param>
		private async Task OnPageSizeChangeAsync(int pageSize)
		{
			// Update the paging
			this.Filter.PageNumber = 1;
			this.Filter.PageSize = pageSize;

			// Update the persons
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
			// Update the ordering
			this.Filter.OrderBy = orderBy;

			// Update the persons
			await this.GetPersonsAsync();
		}

		/// <summary>
		/// Invoked when the order direction changes.
		/// </summary>
		///
		/// <param name="orderDirection">The order direction.</param>
		private async Task OnOrderDirectionChangeAsync(PersonFilterOrderDirection orderDirection)
		{
			// Update the ordering
			this.Filter.OrderDirection = orderDirection;

			// Update the persons
			await this.GetPersonsAsync();
		}
		#endregion

		#region [Methods] Breadcrumb
		/// <summary>
		/// Builds the default breadcrumb.
		/// </summary>
		private void BuildBreadcrumb()
		{
			var name = this.Localizer.GetString(SharedResources.PERSON_PLURAL);

			this.BreadcrumbHeader = this.Localizer.GetString(SharedResources.BREADCRUMB_LIST_HEADER, name);
			this.BuildBreadcrumbLinks();
			this.BuildBreadcrumbActions();
		}

		/// <summary>
		/// Builds the default breadcrumb links from the built-in constants.
		/// </summary>
		private void BuildBreadcrumbLinks()
		{
			this.BreadcrumbLinks = new List<BreadcrumbLink>
			{
				// Previous
				Routes.HomeRoutes.GetRootBreadcrumbLink(),
				// Current
				Routes.PersonRoutes.GetRootBreadcrumbLink()
			};
		}

		/// <summary>
		/// Builds the default breadcrumb actions from the built-in constants.
		/// </summary>
		private void BuildBreadcrumbActions()
		{
			this.BreadcrumbActions = new List<BreadcrumbAction>
			{
				new BreadcrumbAction
				{
					Label =  this.Localizer.GetString(SharedResources.BUTTON_CREATE),
					Tooltip = this.Localizer.GetString(SharedResources.BUTTON_CREATE),
					ButtonClasses = "btn-success",
					IconClasses = "fas fa-plus",
					Enabled = true,
					OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, (arguments) =>
					{
						this.NavigationManager.NavigateTo(Routes.PersonRoutes.CREATE);
					})
				}
			};

			if (this.IsAdministrator().Result == false)
			{
				this.BreadcrumbActions.Clear();
			}
		}
		#endregion
	}
}