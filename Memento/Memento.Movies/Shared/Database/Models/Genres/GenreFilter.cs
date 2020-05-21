using Memento.Shared.Models;

namespace Memento.Movies.Shared.Database.Models.Genres
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