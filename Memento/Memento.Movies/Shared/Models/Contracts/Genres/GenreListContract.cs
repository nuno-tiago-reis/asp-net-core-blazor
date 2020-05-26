using System;

namespace Memento.Movies.Shared.Models.Contracts.Genres
{
	/// <summary>
	/// Implements the 'GenreList' contract.
	/// </summary>
	public sealed class GenreListContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's identifier.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The Genre's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Genre's normalized name.
		/// </summary>
		public string NormalizedName { get; set; }

		/// <summary>
		/// The Genre's created by user identifier.
		/// </summary>
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Genre's created at timestamp.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Genre's updated by user identifier.
		/// </summary>
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Genre's updated at timestamp.
		/// </summary>
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}