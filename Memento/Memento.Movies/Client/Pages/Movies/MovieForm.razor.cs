using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Create)]
	[Route(Routes.MovieRoutes.Update)]
	public sealed partial class MovieForm : MementoComponent<MovieForm>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long? MovieId { get; set; }

		/// <summary>
		/// The movie.
		/// </summary>
		[Parameter]
		public MovieFormContract Movie { get; set; }
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
		/// The save changes modal.
		/// </summary>
		public ConfirmationModal SaveChangesModal { get; set; }

		/// <summary>
		/// The discard changes modal.
		/// </summary>
		public ConfirmationModal DiscardChangesModal { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			if (this.MovieId.HasValue)
			{
				// Get the movie
				var response = await this.MovieService.GetAsync(this.MovieId.Value);
				if (response.Success)
				{
					// Create the contract
					this.Movie = this.Mapper.Map<MovieFormContract>(response.Data);

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
			else
			{
				// Create the contract
				this.Movie = new MovieFormContract();
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

		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnSaveChangesConfirmedAsync()
		{
			if (this.MovieId.HasValue)
			{
				// Update the movie
				var response = await this.MovieService.UpdateAsync(this.MovieId.Value, this.Movie);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, this.MovieId.Value));

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
				// Create the movie
				var response = await this.MovieService.CreateAsync(this.Movie);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, response.Data.Id));

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

		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnDiscardChangesConfirmedAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();

			if (this.MovieId.HasValue)
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, this.MovieId.Value));
			}
			else
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.Root));
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