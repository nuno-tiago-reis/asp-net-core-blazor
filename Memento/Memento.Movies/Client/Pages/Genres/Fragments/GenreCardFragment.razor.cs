using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System;

namespace Memento.Movies.Client.Pages.Genres.Fragments
{
	/// <summary>
	/// Implements the 'GenreCardFragment' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class GenreCardFragment : MementoComponent<GenreCardFragment>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre.
		/// </summary>
		[Parameter]
		public GenreListContract Genre { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Validations
			if (this.Genre == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.Genre)} parameter."
				);
			}
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the view button.
		/// </summary>
		public void OnViewClick()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.Genre.Id));
		}
		#endregion
	}
}