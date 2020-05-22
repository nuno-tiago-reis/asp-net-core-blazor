using Memento.Movies.Shared.Models.Persons;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonCard' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PersonCard : ComponentBase
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
		public Person Person { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person repository.
		/// </summary>
		[Inject]
		public IPersonRepository Repository { get; set; }
		#endregion
	}
}