using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Shared.Components;
using Memento.Shared.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.GenreRoutes.Detail)]
	public sealed partial class GenreDetail : MementoComponent<GenreDetail>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre identifier.
		/// </summary>
		[Parameter]
		public long GenreId { get; set; }

		/// <summary>
		/// The genre.
		/// </summary>
		[Parameter]
		public GenreDetailContract Genre { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre service.
		/// </summary>
		[Inject]
		public IGenreService GenreService { get; set; }
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
			// Get the genre
			var response = await this.GenreService.GetAsync(this.GenreId);
			if (response.Success)
			{
				// Update the genre
				this.Genre = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Navigate to the list
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));

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
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.UpdateIndexed, this.Genre.Id));
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
			var response = await this.GenreService.DeleteAsync(this.Genre.Id);
			if (response.Success)
			{
				// Hide the modal
				await this.ConfirmationModal.HideAsync();

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));

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