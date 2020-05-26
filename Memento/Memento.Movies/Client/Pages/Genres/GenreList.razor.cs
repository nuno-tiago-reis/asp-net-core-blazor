using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.GenreRoutes.Root)]
	public sealed partial class GenreList : MementoComponent<GenreList>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The genre filter.
		/// </summary>
		[Parameter]
		public GenreFilter GenreFilter { get; set; }

		/// <summary>
		/// The genres.
		/// </summary>
		[Parameter]
		public IPage<GenreListContract> Genres { get; set; }
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
			this.Genres = await this.GenreService.GetAllAsync();
		}
		#endregion
	}
}