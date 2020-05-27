using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Repositories.Genres
{
	/// <summary>
	/// Defines the fields over which 'Genres' can be filtered.
	/// </summary
	///
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class GenreFilter : ModelFilter<GenreFilterOrderBy, FilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
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
}