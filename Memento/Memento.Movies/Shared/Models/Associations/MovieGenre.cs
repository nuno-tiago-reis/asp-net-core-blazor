using Memento.Movies.Shared.Models.Genres;
using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Models.Repository;

namespace Memento.Movies.Shared.Models
{
	/// <summary>
	/// Implements the 'MovieGenre' model.
	/// </summary>
	public sealed class MovieGenre : Model
	{
		#region [Properties]
		/// <summary>
		/// The 'Movie' identifier.
		/// </summary>
		public long MovieId { get; set; }

		/// <summary>
		/// The 'Movie' navigation property.
		/// </summary>
		public Movie Movie { get; set; }

		/// <summary>
		/// The 'Genre' identifier.
		/// </summary>
		public long GenreId { get; set; }

		/// <summary>
		/// The 'Genre' navigation property.
		/// </summary>
		public Genre Genre { get; set; }
		#endregion
	}
}