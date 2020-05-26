using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Shared.Components;
using Memento.Shared.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.GenreRoutes.Detail)]
	public sealed partial class GenreDetail : MementoComponent<GenreDetail>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre identifier.
		/// </summary>
		[Parameter]
		public long GenreId { get; set; }

		/// <summary>
		/// The genre.
		/// </summary>
		[Parameter]
		public GenreDetailContract Genre { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre service.
		/// </summary>
		[Inject]
		public IGenreService GenreService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			try
			{
				// Get the genre
				this.Genre = await this.GenreService.GetAsync(this.GenreId);
			}
			catch (MementoException exception)
			{
				if (exception.Type == MementoExceptionType.NotFound)
				{
					// Navigate to the list
					this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));

					// Show the user a notification
					this.Toaster.Error($"The {nameof(this.Genre)} does not exist.");
				}
			}
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Callback that is invoked when the user clicks the update button.
		/// </summary>
		public void OnUpdate()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.UpdateIndexed, this.Genre.Id));
		}

		/// <summary>
		/// Callback that is invoked when the user clicks the delete button.
		/// </summary>
		public async Task OnDeleteAsync()
		{
			// Delete the genre
			await this.GenreService.DeleteAsync(this.Genre.Id);

			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.Root));

			// Show the user a notification
			this.Toaster.Success($"The {nameof(this.Genre)} has been deleted successfully.");
		}
		#endregion
	}
}