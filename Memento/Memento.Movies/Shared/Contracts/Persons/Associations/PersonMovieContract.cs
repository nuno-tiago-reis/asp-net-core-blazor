using Memento.Movies.Shared.Models;
using System;

namespace Memento.Movies.Shared.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonMovie' contract.
	/// </summary>
	public sealed class PersonMovieContract
	{
		#region [Properties]
		/// <summary>
		/// The Movie's role.
		/// </summary>
		public MoviePersonRole Role { get; set; }

		/// <summary>
		/// The Movie's identifier.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The Movie's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Movie's normalized name.
		/// </summary>
		public string NormalizedName { get; set; }

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
		/// The Movie's created by user identifier.
		/// </summary>
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Movie's created at timestamp.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Movie's updated by user identifier.
		/// </summary>
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Movie's updated at timestamp.
		/// </summary>
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}