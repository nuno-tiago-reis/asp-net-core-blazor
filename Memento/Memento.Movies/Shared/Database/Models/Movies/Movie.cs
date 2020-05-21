using Memento.Shared.Models;
using System;

namespace Memento.Movies.Shared.Database.Models.Movies
{
	/// <summary>
	/// Implements the 'Movie' model.
	/// </summary>
	public sealed class Movie : Model
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
		/// The poster url.
		/// </summary>
		public string Poster { get; set; }

		/// <summary>
		/// The release date.
		/// </summary>
		public DateTime ReleaseDate { get; set; }
	}
}