using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Shared.Database.Models.Movies;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Movies
{
	/// <summary>
	/// Implements the 'MovieList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieList : ComponentBase
	{
		#region [Properties]
		/// <summary>
		/// The movies.
		/// </summary>
		[Parameter]
		public List<Movie> Movies { get; set; }

		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }

		/// <summary>
		/// The confirmation modal.
		/// </summary>
		[Parameter]
		public ConfirmationModal ConfirmationModal { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(3000);

			this.Movies = (await this.Repository.GetAllAsync()).ToList();
		}
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