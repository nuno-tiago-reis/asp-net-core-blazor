using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Repositories.Movies
{
	/// <summary>
	/// Defines the fields over which 'Movies' can be filtered.
	/// </summary
	///
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="MovieFilterOrderDirection" />
	public sealed class MovieFilter : ModelFilter<MovieFilterOrderBy, MovieFilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		///  The 'Summary' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_SUMMARY), ResourceType = typeof(SharedResources))]
		public string Summary { get; set; }

		/// <summary>
		///  The 'InTheaters' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_INTHEATERS), ResourceType = typeof(SharedResources))]
		public bool? InTheaters { get; set; }

		/// <summary>
		///  The 'ReleaseDate' filter (released after).
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_RELEASEDAFTER), ResourceType = typeof(SharedResources))]
		public DateTime? ReleasedAfter { get; set; }

		/// <summary>
		///  The 'ReleaseDate' filter (released before).
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_RELEASEDBEFORE), ResourceType = typeof(SharedResources))]
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
		[Display(Name = nameof(SharedResources.MOVIE_ID), ResourceType = typeof(SharedResources))]
		Id = 0,
		/// <summary>
		/// By 'Name'.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_NAME), ResourceType = typeof(SharedResources))]
		Name = 1,
		/// <summary>
		/// By 'ReleaseDate'.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_RELEASEDATE), ResourceType = typeof(SharedResources))]
		ReleaseDate = 2,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_CREATEDAT), ResourceType = typeof(SharedResources))]
		CreatedAt = 3,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		UpdatedAt = 4
	}

	/// <summary>
	/// Defines the direction over which 'Movies' can be ordered by.
	/// </summary>
	public enum MovieFilterOrderDirection
	{
		/// <summary>
		/// In 'Ascending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_ORDER_ASCENDING), ResourceType = typeof(SharedResources))]
		Ascending = 0,
		/// <summary>
		/// In 'Descending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_ORDER_DESCENDING), ResourceType = typeof(SharedResources))]
		Descending = 1
	}
}