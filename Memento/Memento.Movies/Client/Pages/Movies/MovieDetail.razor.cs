using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Database.Models.Movies;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Detail)]
	public sealed partial class MovieDetail : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long MovieId { get; set; }

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

		#region [Properties] References
		/// <summary>
		/// The confirmation modal.
		/// </summary>
		public ConfirmationModal ConfirmationModal { get; set; }
		#endregion

		#region [Methods] ConfirmationModal
		/// <summary>
		/// Callback that is invoked when the confirm button is clicked in the confirmation modal.
		/// </summary>
		private async Task OnConfirm()
		{
			System.Console.WriteLine("Confirm!");
		}

		/// <summary>
		/// Callback that is invoked when the cancel button is clicked in the confirmation modal.
		/// </summary>
		private async Task OnCancel()
		{
			System.Console.WriteLine("Cancel!");
		}
		#endregion
	}
}