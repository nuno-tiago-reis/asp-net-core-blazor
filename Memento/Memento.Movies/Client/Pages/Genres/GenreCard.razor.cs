using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreCard' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class GenreCard : MementoComponent<GenreCard>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre.
		/// </summary>
		[Parameter]
		public GenreListContract Genre { get; set; }
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the view button.
		/// </summary>
		public void OnView()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.Genre.Id));
		}
		#endregion
	}
}