using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Movies
{
	/// <summary>
	/// Defines the fields over which 'Movies' can be filtered.
	/// </summary>
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
		public MovieFilterInTheaters? InTheaters { get; set; }

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

		#region [Methods]
		/// <inheritdoc />
		protected override void ReadFilterFromQuery(Dictionary<string, StringValues> query)
		{
			// Name
			if (query.TryGetValue(nameof(this.Name), out var name))
			{
				this.Name = name;
			}

			// Summary
			if (query.TryGetValue(nameof(this.Summary), out var summary))
			{
				this.Summary = summary;
			}

			// InTheaters
			if (query.TryGetValue(nameof(this.InTheaters), out var inTheatersQuery))
			{
				if (Enum.TryParse(typeof(MovieFilterInTheaters), inTheatersQuery, out var inTheaters))
				{
					this.InTheaters = (MovieFilterInTheaters)inTheaters;
				}
			}

			// ReleasedAfter
			if (query.TryGetValue(nameof(this.ReleasedAfter), out var releasedAfterQuery))
			{
				if (DateTime.TryParse(releasedAfterQuery, out var releasedAfter))
				{
					this.ReleasedAfter = releasedAfter;
				}
			}

			// ReleasedBefore
			if (query.TryGetValue(nameof(this.ReleasedBefore), out var releasedBeforeQuery))
			{
				if (DateTime.TryParse(releasedBeforeQuery, out var releasedBefore))
				{
					this.ReleasedBefore = releasedBefore;
				}
			}
		}

		/// <inheritdoc />
		protected override void WriteFilterToQuery(Dictionary<string, string> query)
		{
			// Name
			if (!string.IsNullOrWhiteSpace(this.Name))
			{
				query.Add(nameof(this.Name), this.Name);
			}

			// Summary
			if (!string.IsNullOrWhiteSpace(this.Summary))
			{
				query.Add(nameof(this.Summary), this.Summary);
			}

			// InTheaters
			if (this.InTheaters != null)
			{
				query.Add(nameof(this.InTheaters), this.InTheaters.Value.ToString());
			}

			// ReleasedAfter
			if (this.ReleasedAfter != null)
			{
				query.Add(nameof(this.ReleasedAfter), this.ReleasedAfter.Value.ToShortDateString());
			}

			// ReleasedBefore
			if (this.ReleasedBefore != null)
			{
				query.Add(nameof(this.ReleasedBefore), this.ReleasedBefore.Value.ToShortDateString());
			}
		}
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

	/// <summary>
	/// Defines the options on which the 'InTheaters' filter can be used.
	/// </summary>
	public enum MovieFilterInTheaters
	{
		/// <summary>
		/// In 'Checked' option.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_INTHEATERS_CHECKED), ResourceType = typeof(SharedResources))]
		Checked = 0,
		/// <summary>
		/// In 'Unchecked' option.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_INTHEATERS_UNCHECKED), ResourceType = typeof(SharedResources))]
		Unchecked = 1
	}
}