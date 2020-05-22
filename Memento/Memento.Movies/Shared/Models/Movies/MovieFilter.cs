using Memento.Shared.Models;
using System;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Defines the fields over which 'Movies' can be filtered.
	/// </summary
	///
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class MovieFilter : ModelFilter<MovieFilterOrderBy, FilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///  The 'Summary' filter.
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		///  The 'InTheaters' filter.
		/// </summary>
		public bool? InTheaters { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (released after).
		/// </summary>
		public DateTime? ReleasedAfter { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (released before).
		/// </summary>
		public DateTime? ReleasedBefore { get; set; }
		#endregion
	}

	/// <summary>
	/// Defines the fields over which 'Movies' can be ordered by.
	/// </summary>
	public enum MovieFilterOrderBy
	{
		/// <summary>
		/// By 'Id'.
		/// </summary>
		Id = 0,
		/// <summary>
		/// By 'Name'.
		/// </summary>
		Name = 1,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		CreatedAt = 2,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		UpdatedAt = 3
	}
}