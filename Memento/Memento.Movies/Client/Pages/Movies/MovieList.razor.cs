using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Root)]
	public sealed partial class MovieList : MementoComponent<MovieList>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie filter.
		/// </summary>
		[Parameter]
		public MovieFilter MovieFilter { get; set; }

		/// <summary>
		/// The movies.
		/// </summary>
		[Parameter]
		public IPage<MovieListContract> Movies { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie service.
		/// </summary>
		[Inject]
		public IMovieService MovieService { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			var response = await this.MovieService.GetAllAsync();
			if (response.Success)
			{
				// Update the movies
				this.Movies = response.Data;

				// Show a toast message
				this.Toaster.Success(response.Message);
			}
			else
			{
				// Clear the movies
				this.Movies = null;

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_LOADING));
			}
		}
		#endregion
	}
}