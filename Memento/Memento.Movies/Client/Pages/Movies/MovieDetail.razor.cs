using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Detail)]
	public sealed partial class MovieDetail : MementoComponent<MovieDetail>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long MovieId { get; set; }

		/// <summary>
		/// The movie.
		/// </summary>
		[Parameter]
		public MovieDetailContract Movie { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie service.
		/// </summary>
		[Inject]
		public IMovieService MovieService { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The confirmation modal.
		/// </summary>
		public ConfirmationModal ConfirmationModal { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			// Get the person
			var response = await this.MovieService.GetAsync(this.MovieId);
			if (response.Success)
			{
				// Update the person
				this.Movie = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Navigate to the list
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.Root));

				// Show a toast message
				this.Toaster.Error(response.Message);
			}
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the update button.
		/// </summary>
		public void OnUpdate()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.UpdateIndexed, this.Movie.Id));
		}

		/// <summary>
		/// Callback that is invoked when the user clicks the delete button.
		/// </summary>
		public async Task OnDeleteAsync()
		{
			// Show the modal
			await this.ConfirmationModal.ShowAsync();
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the delete modal.
		/// </summary>
		public async Task OnDeleteConfirmedAsync()
		{
			// Delete the genre
			var response = await this.MovieService.DeleteAsync(this.Movie.Id);
			if (response.Success)
			{
				// Hide the modal
				await this.ConfirmationModal.HideAsync();

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.Root));

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Hide the modal
				await this.ConfirmationModal.HideAsync();

				// Show a toast message
				this.Toaster.Error(response.Message);
			}
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the cancel button in the delete modal.
		/// </summary>
		public async Task OnDeleteCancelledAsync()
		{
			// Hide the modal
			await this.ConfirmationModal.HideAsync();
		}
		#endregion
	}
}