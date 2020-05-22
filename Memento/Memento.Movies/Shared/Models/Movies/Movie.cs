using Memento.Shared.Models;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Implements the 'Movie' model.
	/// </summary>
	public sealed class Movie : Model
	{
		#region [Properties]
		/// <summary>
		/// The name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The normalized name.
		/// </summary>
		public string NormalizedName { get; set; }

		/// <summary>
		/// The summary.
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// The picture url.
		/// </summary>
		public string PictureUrl { get; set; }

		/// <summary>
		/// The trailer url.
		/// </summary>
		public string TrailerUrl { get; set; }

		/// <summary>
		/// The release date.
		/// </summary>
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Whether the movie is in theaters.
		/// </summary>
		public bool InTheaters { get; set; }

		/// <summary>
		/// The genres associated with the movie.
		/// </summary>
		public List<MovieGenre> Genres { get; set; }

		/// <summary>
		/// The persons associated with the movie.
		/// </summary>
		public List<MoviePerson> Persons { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override bool Equals(object @object)
		{
			if (@object is Movie movie)
			{
				return this.Id == movie.Id;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}