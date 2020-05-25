using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Components;
using Memento.Shared.Pagination;
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
	public sealed partial class MovieList : BaseComponent<MovieList>
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
		public IPage<Movie> Movies { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(1000);

			this.Movies = await this.Repository.GetAllAsync();
		}
		#endregion
	}
}