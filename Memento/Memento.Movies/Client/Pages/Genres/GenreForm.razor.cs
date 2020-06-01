using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
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

		#region [Properties] Internal
		/// <summary>
		/// The genre.
		/// </summary>
		private GenreDetailContract Genre { get; set; }

		/// <summary>
		/// The genre changes.
		/// </summary>
		private GenreFormContract GenreChanges { get; set; }

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
		/// The genre service.
		/// </summary>
		[Inject]
		private IGenreService GenreService { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The edit form.
		/// </summary>
		private EditForm EditForm { get; set; }

		/// <summary>
		/// The edit context.
		/// </summary>
		private EditContext EditContext { get; set; }

		/// <summary>
		/// The save changes modal.
		/// </summary>
		private ConfirmationModal SaveChangesModal { get; set; }

		/// <summary>
		/// The discard changes modal.
		/// </summary>
		private ConfirmationModal DiscardChangesModal { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			// Get the genre
			await this.GetGenre();

			// Build the context
			this.BuildContext();

			// Build the breadcrumb
			this.BuildBreadcrumb();
		}
		#endregion

		#region [Methods] Data
		/// <summary>
		/// Gets the genre from the backend if the identifier is provided.
		/// Otherwise creates a new genre with no data.
		/// </summary>
		private async Task GetGenre()
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
		/// Builds the forms edit context.
		/// </summary>
		private void BuildContext()
		{
			this.EditContext = new EditContext(this.GenreChanges);
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with no errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		private async Task OnValidSubmitAsync(EditContext _)
		{
			await this.SaveChangesModal.ShowAsync();
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		private void OnInvalidSubmit(EditContext _)
		{
			// Show a toast message
			this.Toaster.Error(this.Localizer.GetString(SharedResources.FORM_INVALID_FIELDS));
		}

		/// <summary>
		/// Callback that is invoked when the form is cancelled.
		/// </summary>
		private async Task OnCancelAsync()
		{
			await this.DiscardChangesModal.ShowAsync();
		}
		#endregion

		#region [Methods] Save Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		private async Task OnSaveChangesConfirmedAsync()
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
		private async Task OnSaveChangesCancelledAsync()
		{
			// Hide the modal
			await this.SaveChangesModal.HideAsync();
		}
		#endregion

		#region [Methods] Discard Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		private async Task OnDiscardChangesConfirmedAsync()
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
		private async Task OnDiscardChangesCancelledAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();
		}
		#endregion

		#region [Methods] Breadcrumb
		/// <summary>
		/// Builds the default breadcrumb.
		/// </summary>
		private void BuildBreadcrumb()
		{
			var name = this.GenreId.HasValue == false
				? this.Localizer.GetString(SharedResources.GENRE)
				: this.Genre.Name;
			var header = this.GenreId.HasValue == false
				? this.Localizer.GetString(SharedResources.BREADCRUMB_CREATE_HEADER, name)
				: this.Localizer.GetString(SharedResources.BREADCRUMB_UPDATE_HEADER, name);

			this.BreadcrumbHeader = header;
			this.BuildBreadcrumbLinks();
			this.BuildBreadcrumbActions();
		}

		/// <summary>
		/// Builds the default breadcrumb links from the built-in constants.
		/// </summary>
		private void BuildBreadcrumbLinks()
		{
			if (this.GenreId.HasValue == false)
			{
				this.BreadcrumbLinks = new List<BreadcrumbLink>
				{
					// Previous
					Routes.HomeRoutes.GetRootBreadcrumbLink(),
					// Previous
					Routes.GenreRoutes.GetRootBreadcrumbLink(),
					// Current
					Routes.GenreRoutes.GetCreateBreadcrumbLink()
				};
			}
			else
			{
				this.BreadcrumbLinks = new List<BreadcrumbLink>
				{
					// Previous
					Routes.HomeRoutes.GetRootBreadcrumbLink(),
					// Previous
					Routes.GenreRoutes.GetRootBreadcrumbLink(),
					// Previous
					Routes.GenreRoutes.GetDetailBreadcrumbLink(this.GenreId.Value),
					// Current
					Routes.GenreRoutes.GetUpdateBreadcrumbLink(this.GenreId.Value)
				};
			}
		}

		/// <summary>
		/// Builds the default breadcrumb actions from the built-in constants.
		/// </summary>
		private void BuildBreadcrumbActions()
		{
			this.BreadcrumbActions = new List<BreadcrumbAction>()
			{
				new BreadcrumbAction
				{
					Label = this.Localizer.GetString(SharedResources.BUTTON_SAVE_CHANGES),
					Tooltip = this.Localizer.GetString(SharedResources.BUTTON_SAVE_CHANGES),
					ButtonClasses = "btn-success",
					IconClasses = "fas fa-save",
					Enabled = true,
					OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async (arguments) =>
					{
						System.Console.WriteLine(this.EditForm);
						System.Console.WriteLine(this.EditForm.Model);
						System.Console.WriteLine(this.EditForm.EditContext);

						await this.EditForm.SubmitAsync();
					})
				},
				new BreadcrumbAction
				{
					Label = this.Localizer.GetString(SharedResources.BUTTON_DISCARD_CHANGES),
					Tooltip = this.Localizer.GetString(SharedResources.BUTTON_DISCARD_CHANGES),
					ButtonClasses = "btn-danger",
					IconClasses = "fas fa-times",
					Enabled = true,
					OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async (arguments) =>
					{
						await this.OnCancelAsync();
					})
				}
			};
		}
		#endregion
	}
}