using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.PersonRoutes.Root)]
	public sealed partial class PersonList : MementoComponent<PersonList>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person filter.
		/// </summary>
		[Parameter]
		public PersonFilter PersonFilter { get; set; }

		/// <summary>
		/// The persons.
		/// </summary>
		[Parameter]
		public IPage<PersonListContract> Persons { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person service.
		/// </summary>
		[Inject]
		public IPersonService PersonService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			var response = await this.PersonService.GetAllAsync();
			if (response.Success)
			{
				// Update the persons
				this.Persons = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the persons
				this.Persons = null;

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_LOADING));
			}
		}
		#endregion
	}
}