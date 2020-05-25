using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Shared.Components;
using Memento.Shared.Exceptions;
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
	public sealed partial class GenreForm : BaseComponent<GenreForm>
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
		public GenreFormContract Genre { get; set; }
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
			if (this.GenreId.HasValue == false)
			{
				// Create the contract
				this.Genre = new GenreFormContract();
			}
			else
			{
				try
				{
					// Get the genre
					var genre = await this.GenreService.GetAsync(this.GenreId.Value);

					// Create the contract
					this.Genre = this.Mapper.Map<GenreFormContract>(genre);
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
		}
		#endregion

		#region [Methods] Form
		/// <summary>
		/// Callback that is invoked when the form is submited with no errors
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnValidSubmitAsync(EditContext _)
		{
			if (this.GenreId.HasValue == false)
			{
				// Create the genre
				var genre = await this.GenreService.CreateAsync(this.Genre);

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, genre.Id));

				// Show the user a notification
				this.Toaster.Success($"The {nameof(this.Genre)} has been created successfully.");
			}
			else
			{
				// Update the genre
				await this.GenreService.UpdateAsync(this.GenreId.Value, this.Genre);

				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.GenreId.Value));

				// Show the user a notification
				this.Toaster.Success($"The {nameof(this.Genre)} has been updated successfully.");
			}
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public void OnInvalidSubmit(EditContext _)
		{
			this.Toaster.Error("The form has invalid fields.");
		}

		/// <summary>
		/// Callback that is invoked when the form is cancelled.
		/// </summary>
		public void OnCancel()
		{
			// Navigate to the detail
			this.NavigationManager.NavigateTo(string.Format(Routes.GenreRoutes.DetailIndexed, this.GenreId.Value));
		}
		#endregion
	}
}