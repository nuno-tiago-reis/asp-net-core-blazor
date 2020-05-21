using Memento.Movies.Shared.Database.Models.Persons;
using Memento.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PersonList : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person filter.
		/// </summary>
		[Parameter]
		public PersonFilter PersonFilter { get; set; }

		/// <summary>
		/// The personsS.
		/// </summary>
		[Parameter]
		public IModelPage<Person> Persons { get; set; }
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