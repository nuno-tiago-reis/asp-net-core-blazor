using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Pagination;
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
	public sealed partial class PersonList : ComponentBase
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
		public IPage<Person> Persons { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The person repository.
		/// </summary>
		[Inject]
		public IPersonRepository Repository { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(1000);

			this.Persons = await this.Repository.GetAllAsync();
		}
		#endregion
	}
}