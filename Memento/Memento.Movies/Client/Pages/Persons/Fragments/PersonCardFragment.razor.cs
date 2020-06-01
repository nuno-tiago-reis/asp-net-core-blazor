using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Persons.Fragments
{
	/// <summary>
	/// Implements the 'PersonCardFragment' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PersonCardFragment : MementoComponent<PersonCardFragment>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person.
		/// </summary>
		[Parameter]
		public PersonListContract Person { get; set; }
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the view button.
		/// </summary>
		public void OnView()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.DetailIndexed, this.Person.Id));
		}
		#endregion
	}
}