using Memento.Movies.Shared.Database.Models.Genres;
using Memento.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
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
		public IModelPage<Genre> Genres { get; set; }
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