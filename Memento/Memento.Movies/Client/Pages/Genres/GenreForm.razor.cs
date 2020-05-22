using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Database.Models.Genres;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
	public sealed partial class GenreForm : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre identifier.
		/// </summary>
		[Parameter]
		public long? GenreId { get; set; }

		/// <summary>
		/// The genre.
		/// </summary>
		[Parameter]
		public Genre Genre { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The genre form.
		/// </summary>
		public EditForm Form { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre repository.
		/// </summary>
		[Inject]
		public IGenreRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="GenreForm"/> class.
		/// </summary>
		public GenreForm()
		{
			this.Genre = new Genre();
		}
		#endregion

		#region [Methods] Form
		/// <summary>
		/// Callback that is invoked when the form is submited with no errors
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnValidSubmitAsync(EditContext context)
		{
			await Task.Delay(1000);

			System.Console.WriteLine("OnValidSubmitAsync");
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnInvalidSubmitAsync(EditContext context)
		{
			await Task.Delay(1000);

			System.Console.WriteLine("OnInvalidSubmitAsync");
		}

		/// <summary>
		/// Callback that is invoked when the form is cancelled.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnCancelAsync(EditContext context)
		{
			await Task.Delay(1000);

			System.Console.WriteLine("OnCancelAsync");
		}

		/// <summary>
		/// Returns whether this form is in create mode.
		/// </summary>
		public bool IsCreate()
		{
			return this.Genre == null || this.Genre.Id == 0;
		}

		/// <summary>
		/// Returns whether this form is in update mode.
		/// </summary>
		public bool IsUpdate()
		{
			return this.Genre != null && this.Genre.Id != 0;
		}
		#endregion
	}
}