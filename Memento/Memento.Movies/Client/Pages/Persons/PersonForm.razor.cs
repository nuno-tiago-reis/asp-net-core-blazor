using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.PersonRoutes.Create)]
	[Route(Routes.PersonRoutes.Update)]
	public sealed partial class PersonForm : BaseComponent<PersonForm>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person identifier.
		/// </summary>
		[Parameter]
		public long? PersonId { get; set; }

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

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonForm"/> class.
		/// </summary>
		public PersonForm()
		{
			this.Person = new Person();
		}
		#endregion

		#region [Methods] Form
		/// <summary>
		/// Callback that is invoked when the form is submited with no errors
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public async Task OnValidSubmitAsync(EditContext context)
		{
			await Task.Delay(1000);
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public async Task OnInvalidSubmitAsync(EditContext context)
		{
			await Task.Delay(1000);
		}
		#endregion
	}
}