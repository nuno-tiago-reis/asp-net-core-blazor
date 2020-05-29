using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Repositories;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages
{
	/// <summary>
	/// Implements the 'Index' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.HomeRoutes.Root)]
	public sealed partial class Index : MementoComponent<Index>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movies in theaters filter.
		/// </summary>
		public MovieFilter InTheatersFilter { get; set; }

		/// <summary>
		/// The movies in theaters.
		/// </summary>
		public IPage<MovieListContract> InTheaters { get; set; }

		/// <summary>
		/// The upcoming movie releases filter.
		/// </summary>
		public MovieFilter UpcomingReleasesFilter { get; set; }

		/// <summary>
		/// The upcoming movie releases.
		/// </summary>
		public IPage<MovieListContract> UpcomingReleases { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie service.
		/// </summary>
		[Inject]
		public IMovieService MovieService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			// Build the in theaters filter
			this.InTheatersFilter = new MovieFilter
			{
				InTheaters = true,
				ReleasedBefore = DateTime.Today,
				PageNumber = 1,
				PageSize = 3,
				OrderBy = MovieFilterOrderBy.ReleaseDate,
				OrderDirection = FilterOrderDirection.Descending
			};

			// Invoke the API
			var response = await this.MovieService.GetAllAsync(this.InTheatersFilter);
			if (response.Success)
			{
				// Update the movies
				this.InTheaters = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the movies
				this.InTheaters = null;

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_LOADING));
			}

			// Build the in upcoming releases filter
			this.UpcomingReleasesFilter = new MovieFilter
			{
				InTheaters = false,
				ReleasedAfter = DateTime.Today,
				PageNumber = 1,
				PageSize = 3,
				OrderBy = MovieFilterOrderBy.ReleaseDate,
				OrderDirection = FilterOrderDirection.Ascending
			};

			// Invoke the API
			response = await this.MovieService.GetAllAsync(this.UpcomingReleasesFilter);
			if (response.Success)
			{
				// Update the movies
				this.UpcomingReleases = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the movies
				this.InTheaters = null;

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_LOADING));
			}
		}
		#endregion
	}
}