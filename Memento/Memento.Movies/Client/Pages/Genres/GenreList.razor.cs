using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Database.Models.Genres;
using Memento.Shared.Pagination;
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
	public sealed partial class GenreList : ComponentBase
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
		public IPage<Genre> Genres { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre repository.
		/// </summary>
		[Inject]
		public IGenreRepository Repository { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(1000);

			this.Genres = await this.Repository.GetAllAsync();
		}
		#endregion
	}
}