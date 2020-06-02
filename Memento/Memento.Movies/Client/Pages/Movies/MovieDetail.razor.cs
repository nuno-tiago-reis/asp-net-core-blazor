using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieDetail' component.
	/// </summary>
	///
	/// <seealso cref="MementoComponent{MovieDetail}"/>
	[Authorize]
	[Route(Routes.MovieRoutes.DETAIL)]
	public sealed partial class MovieDetail : MementoComponent<MovieDetail>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long MovieId { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The movie.
		/// </summary>
		private MovieDetailContract Movie { get; set; }

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
		/// The movie service.
		/// </summary>
		[Inject]
		private IMovieService MovieService { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The confirmation modal.
		/// </summary>
		private ConfirmationModal ConfirmationModal { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override async Task OnInitializedAsync()
		{
			// Get the movie
			await this.GetMovie();

			// Build the breadcrumb
			this.BuildBreadcrumb();
		}
		#endregion

		#region [Methods] Data
		/// <summary>
		/// Gets the movie from the backend if the identifier is provided.
		/// Otherwise creates a new movie with no data.
		/// </summary>
		private async Task GetMovie()
		{
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
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.ROOT));

				// Show a toast message
				this.Toaster.Error(response.Message);
			}
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the update button.
		/// </summary>
		private void OnUpdate()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.UPDATE_INDEXED, this.Movie.Id));
		}

		/// <summary>
		/// Callback that is invoked when the user clicks the delete button.
		/// </summary>
		private async Task OnDeleteAsync()
		{
			// Show the modal
			await this.ConfirmationModal.ShowAsync();
		}
		#endregion

		#region [Methods] Delete Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the delete modal.
		/// </summary>
		private async Task OnDeleteConfirmedAsync()
		{
			// Delete the genre
			var response = await this.MovieService.DeleteAsync(this.Movie.Id);
			if (response.Success)
			{
				// Hide the modal
				await this.ConfirmationModal.HideAsync();

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.ROOT));

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
		private async Task OnDeleteCancelledAsync()
		{
			// Hide the modal
			await this.ConfirmationModal.HideAsync();
		}
		#endregion

		#region [Methods] Breadcrumb
		/// <summary>
		/// Builds the default breadcrumb.
		/// </summary>
		private void BuildBreadcrumb()
		{
			var name = this.Movie.Name;

			this.BreadcrumbHeader = this.Localizer.GetString(SharedResources.BREADCRUMB_DETAIL_HEADER, name);
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
				// Previous
				Routes.MovieRoutes.GetRootBreadcrumbLink(),
				// Current
				Routes.MovieRoutes.GetDetailBreadcrumbLink(this.MovieId)
			};
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
					Label = this.Localizer.GetString(SharedResources.BUTTON_UPDATE),
					Tooltip = this.Localizer.GetString(SharedResources.BUTTON_UPDATE),
					ButtonClasses = "btn-success",
					IconClasses = "fas fa-edit",
					Enabled = true,
					OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, (arguments) =>
					{
						this.OnUpdate();
					})
				},
				new BreadcrumbAction
				{
					Label = this.Localizer.GetString(SharedResources.BUTTON_DELETE),
					Tooltip = this.Localizer.GetString(SharedResources.BUTTON_DELETE),
					ButtonClasses = "btn-danger",
					IconClasses = "fas fa-trash",
					Enabled = true,
					OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async (arguments) =>
					{
						await this.OnDeleteAsync();
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