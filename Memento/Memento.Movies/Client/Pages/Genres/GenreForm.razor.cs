using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.GenreRoutes.Create)]
	[Route(Routes.GenreRoutes.Update)]
	public sealed partial class GenreForm : MementoComponent<GenreForm>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre identifier.
		/// </summary>
		[Parameter]
		public long? GenreId { get; set; }
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
		/// The save changes modal.
		/// </summary>
		public ConfirmationModal SaveChangesModal { get; set; }

		/// <summary>
		/// The discard changes modal.
		/// </summary>
		public ConfirmationModal DiscardChangesModal { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The genre.
		/// </summary>
		private GenreDetailContract Genre { get; set; }

		/// <summary>
		/// The genre changes.
		/// </summary>
		private GenreFormContract GenreChanges { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			if (this.GenreId.HasValue)
			{
				// Get the genre
				var response = await this.GenreService.GetAsync(this.GenreId.Value);
				if (response.Success)
				{
					// Create the contracts
					this.Genre = response.Data;
					this.GenreChanges = this.Mapper.Map<GenreFormContract>(response.Data);

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Create the contracts
					this.Genre = response.Data;
					this.GenreChanges = this.Mapper.Map<GenreFormContract>(response.Data);

					// Navigate to the list
					this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));

					// Show a toast message
					this.Toaster.Error(response.Message);
				}
			}
			else
			{
				// Create the contracts
				this.Genre = new GenreDetailContract();
				this.GenreChanges = new GenreFormContract();
			}
		}
		#endregion

		#region [Methods] Form
		/// <summary>
		/// Callback that is invoked when the form is submited with no errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnValidSubmitAsync(EditContext _)
		{
			await this.SaveChangesModal.ShowAsync();
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public void OnInvalidSubmit(EditContext _)
		{
			// Show a toast message
			this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_FORM_INVALID_FIELDS));
		}

		/// <summary>
		/// Callback that is invoked when the form is cancelled.
		/// </summary>
		public async Task OnCancelAsync()
		{
			await this.DiscardChangesModal.ShowAsync();
		}
		#endregion

		#region [Methods] Save Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnSaveChangesConfirmedAsync()
		{
			if (this.GenreId.HasValue)
			{
				// Update the genre
				var response = await this.GenreService.UpdateAsync(this.GenreId.Value, this.GenreChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.GenreId.Value));

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Show a toast message
					this.Toaster.Error(string.Join("\r\n", response.Errors), response.Message);
				}
			}
			else
			{
				// Create the genre
				var response = await this.GenreService.CreateAsync(this.GenreChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, response.Data.Id));

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Show a toast message
					this.Toaster.Error(string.Join("\r\n", response.Errors), response.Message);
				}
			}
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the cancel button in the save changes modal.
		/// </summary>
		public async Task OnSaveChangesCancelledAsync()
		{
			// Hide the modal
			await this.SaveChangesModal.HideAsync();
		}
		#endregion

		#region [Methods] Discard Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnDiscardChangesConfirmedAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();

			if (this.GenreId.HasValue)
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.GenreId.Value));
			}
			else
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));
			}
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the cancel button in the discard changes modal.
		/// </summary>
		public async Task OnDiscardChangesCancelledAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();
		}
		#endregion
	}
}