using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
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
			this.Filter = new GenreFilter
			{
				PageNumber = INITIAL_PAGE_NUMBER,
				PageSize = INITIAL_PAGE_SIZE,
				OrderBy = GenreFilterOrderBy.Name,
				OrderDirection = GenreFilterOrderDirection.Ascending
			};

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

				// Update the filter
				this.Filter = this.Mapper.Map<GenreFilter>(response.Data);

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
			await this.GetGenresAsync();
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
			// Update the filter
			this.Filter.OrderBy = orderBy;

			// Update the movies
			await this.GetGenresAsync();
		}

		/// <summary>
		/// Invoked when the order direction changes.
		/// </summary>
		///
		/// <param name="orderDirection">The order direction.</param>
		private async Task OnOrderDirectionChangeAsync(GenreFilterOrderDirection orderDirection)
		{
			// Update the filter
			this.Filter.OrderDirection = orderDirection;

			// Update the movies
			await this.GetGenresAsync();
		}
		#endregion
	}
}