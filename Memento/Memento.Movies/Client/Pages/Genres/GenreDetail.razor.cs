using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Database.Models.Genres;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreDetail' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.GenreRoutes.Detail)]
	public sealed partial class GenreDetail : ComponentBase
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
		/// The genre repository.
		/// </summary>
		[Inject]
		public IGenreRepository Repository { get; set; }
		#endregion
	}
}