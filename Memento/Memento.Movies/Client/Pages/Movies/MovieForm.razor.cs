using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Create)]
	[Route(Routes.MovieRoutes.Update)]
	public sealed partial class MovieForm : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long? MovieId { get; set; }

		/// <summary>
		/// The movie.
		/// </summary>
		[Parameter]
		public Movie Movie { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieForm"/> class.
		/// </summary>
		public MovieForm()
		{
			this.Movie = new Movie();
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
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnInvalidSubmitAsync(EditContext context)
		{
			await Task.Delay(1000);
		}
		#endregion
	}
}