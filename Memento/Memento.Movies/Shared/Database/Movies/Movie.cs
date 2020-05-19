using System;

namespace Memento.Movies.Shared.Database.Movies
{
	/// <summary>
	/// Implements the 'Movie' database entity.
	/// </summary>
	public sealed class Movie
	{
		/// <summary>
		/// The title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The genre.
		/// </summary>
		public string Genre { get; set; }

		/// <summary>
		/// The release date.
		/// </summary>
		public DateTime ReleaseDate { get; set; }
	}
}