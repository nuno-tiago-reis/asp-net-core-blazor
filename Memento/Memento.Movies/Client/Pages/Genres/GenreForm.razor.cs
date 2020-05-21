using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Genres
{
	/// <summary>
	/// Implements the 'GenreForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class GenreForm : ComponentBase
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
		//[Parameter]
		//public Genre Genre { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre repository.
		/// </summary>
		//[Inject]
		//public IGenreRepository Repository { get; set; }
		#endregion

	}
}