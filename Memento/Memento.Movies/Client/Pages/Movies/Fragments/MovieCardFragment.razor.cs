using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Movies.Fragments
{
	/// <summary>
	/// Implements the 'MovieCardFragment' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieCardFragment : MementoComponent<MovieCardFragment>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie.
		/// </summary>
		[Parameter]
		public MovieListContract Movie { get; set; }
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the view button.
		/// </summary>
		public void OnView()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DETAIL_INDEXED, this.Movie.Id));
		}
		#endregion
	}
}