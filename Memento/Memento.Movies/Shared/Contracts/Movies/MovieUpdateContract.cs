using Memento.Movies.Shared.Models;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieUpdate' contract.
	/// </summary>
	public sealed class MovieUpdateContract
	{
		#region [Properties]
		/// <summary>
		/// The Movie's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Movie's summary.
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// The Movie's picture url.
		/// </summary>
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Movie's trailer url.
		/// </summary>
		public string TrailerUrl { get; set; }

		/// <summary>
		/// The Movie's release date.
		/// </summary>
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Whether the Movie is in theaters.
		/// </summary>
		public bool InTheaters { get; set; }

		/// <summary>
		/// The Genres associated with the Movie.
		/// </summary>
		public List<long> Genres { get; set; }

		/// <summary>
		/// The Persons associated with the Movie.
		/// </summary>
		public List<Tuple<long, MoviePersonRole>> Persons { get; set; }
		#endregion
	}
}