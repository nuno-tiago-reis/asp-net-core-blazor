using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.PersonRoutes.Detail)]
	public sealed partial class PersonDetail : MementoComponent<PersonDetail>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person identifier.
		/// </summary>
		[Parameter]
		public long PersonId { get; set; }

		/// <summary>
		/// The person.
		/// </summary>
		[Parameter]
		public PersonDetailContract Person { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person service.
		/// </summary>
		[Inject]
		public IPersonService PersonService { get; set; }
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
			var response = await this.PersonService.GetAsync(this.PersonId);
			if (response.Success)
			{
				// Update the person
				this.Person = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Navigate to the list
				this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.Root));

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
			this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.UpdateIndexed, this.Person.Id));
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
			// Delete the person
			var response = await this.PersonService.DeleteAsync(this.Person.Id);
			if (response.Success)
			{
				// Hide the modal
				await this.ConfirmationModal.HideAsync();

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.Root));

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