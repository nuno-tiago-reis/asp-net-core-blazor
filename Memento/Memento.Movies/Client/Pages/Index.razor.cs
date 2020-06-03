using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages
{
	/// <summary>
	/// Implements the 'Index' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[AllowAnonymous]
	[Route(Routes.HomeRoutes.ROOT)]
	public sealed partial class Index : MementoComponent<Index>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movies in theaters.
		/// </summary>
		public Page<MovieListContract> InTheaters { get; set; }

		/// <summary>
		/// The upcoming movie releases.
		/// </summary>
		public Page<MovieListContract> UpcomingReleases { get; set; }
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
		protected override async Task OnInitializedAsync()
		{
			// Invoke the API
			var response = await this.MovieService.GetInTheatersAsync();
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

			// Invoke the API
			response = await this.MovieService.GetUpcomingReleasesAsync();
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