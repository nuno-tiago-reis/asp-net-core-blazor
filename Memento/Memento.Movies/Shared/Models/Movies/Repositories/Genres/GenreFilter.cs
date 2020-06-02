using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Genres
{
	/// <summary>
	/// Defines the fields over which 'Genres' can be filtered.
	/// </summary>
	///
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="GenreFilterOrderDirection" />
	public sealed class GenreFilter : ModelFilter<GenreFilterOrderBy, GenreFilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
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
		}

		/// <inheritdoc />
		protected override void WriteFilterToQuery(Dictionary<string, string> query)
		{
			// Name
			if (!string.IsNullOrWhiteSpace(this.Name))
			{
				query.Add(nameof(this.Name), this.Name);
			}
		}
		#endregion
	}

	/// <summary>
	/// Defines the fields over which 'Genres' can be ordered by.
	/// </summary>
	public enum GenreFilterOrderBy
	{
		/// <summary>
		/// By 'Id'.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ID), ResourceType = typeof(SharedResources))]
		Id = 0,
		/// <summary>
		/// By 'Name'.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		Name = 1,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_CREATEDAT), ResourceType = typeof(SharedResources))]
		CreatedAt = 2,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		UpdatedAt = 3
	}

	/// <summary>
	/// Defines the direction over which 'Genres' can be ordered by.
	/// </summary>
	public enum GenreFilterOrderDirection
	{
		/// <summary>
		/// In 'Ascending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ORDER_ASCENDING), ResourceType = typeof(SharedResources))]
		Ascending = 0,
		/// <summary>
		/// In 'Descending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ORDER_DESCENDING), ResourceType = typeof(SharedResources))]
		Descending = 1
	}
}