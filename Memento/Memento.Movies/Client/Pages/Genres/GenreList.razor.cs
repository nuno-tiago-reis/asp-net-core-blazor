using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreList' component.
	/// </summary>
	/// 
	/// <seealso cref="MementoComponent{}"/>
	[Route(Routes.GenreRoutes.Root)]
	public sealed partial class GenreList : MementoComponent<GenreList>
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
		/// The genres.
		/// </summary>
		[Parameter]
		public Page<GenreListContract> Genres { get; set; }

		/// <summary>
		/// The filter.
		/// </summary>
		[Parameter]
		public GenreFilter Filter { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre service.
		/// </summary>
		[Inject]
		public IGenreService GenreService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			// Initialize the filter
			this.GetFilter();

			// Initalize the movies
			await this.GetGenresAsync();
		}
		#endregion

		#region [Methods] Search
		/// <summary>
		/// Gets the movies from the API using the filter.
		/// </summary>
		private async Task GetGenresAsync()
		{
			// Invoke the API
			var response = await this.GenreService.GetAllAsync(this.Filter);
			if (response.Success)
			{
				// Update the movies
				this.Genres = response.Data;

				// Update the paging
				this.Mapper.Map(response.Data, this.Filter);

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the movies
				this.Genres = null;

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
		/// Gets the filter from the query string.
		/// </summary>
		private void GetFilter()
		{
			// Get the query from the url
			var query = QueryHelpers.ParseQuery(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query);

			// Create the filter
			this.Filter = new GenreFilter
			{
				PageNumber = INITIAL_PAGE_NUMBER,
				PageSize = INITIAL_PAGE_SIZE,
				OrderBy = GenreFilterOrderBy.Name,
				OrderDirection = GenreFilterOrderDirection.Ascending
			};

			// Parse the query
			this.Filter.ReadFromQuery(query);
		}

		/// <summary>
		/// Callback that is invoked when the filter is applied.
		/// </summary>
		private async Task OnFilterSearchAsync(GenreFilter _)
		{
			// Reset the paging
			this.Filter.PageNumber = 1;
			this.Filter.PageSize = this.Filter.PageSize;

			// Update the genres
			await this.GetGenresAsync();
		}

		/// <summary>
		/// Callback that is invoked when the filter is reset.
		/// </summary>
		private async Task OnFilterResetAsync(GenreFilter _)
		{
			// Reset the filter
			this.Filter = new GenreFilter();

			// Update the genres
			await this.GetGenresAsync();
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

			// Update the genres
			await this.GetGenresAsync();
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

			// Update the genres
			await this.GetGenresAsync();
		}
		#endregion

		#region [Methods] Ordering
		/// <summary>
		/// Invoked when the order by changes.
		/// </summary>
		///
		/// <param name="orderBy">The order by.</param>
		private async Task OnOrderByChangeAsync(GenreFilterOrderBy orderBy)
		{
			// Update the ordering
			this.Filter.OrderBy = orderBy;

			// Update the genres
			await this.GetGenresAsync();
		}

		/// <summary>
		/// Invoked when the order direction changes.
		/// </summary>
		///
		/// <param name="orderDirection">The order direction.</param>
		private async Task OnOrderDirectionChangeAsync(GenreFilterOrderDirection orderDirection)
		{
			// Update the ordering
			this.Filter.OrderDirection = orderDirection;

			// Update the genres
			await this.GetGenresAsync();
		}
		#endregion
	}
}