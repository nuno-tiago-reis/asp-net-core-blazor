using Memento.Shared.Models;

namespace Memento.Movies.Shared.Database.Models.Movies
{
	/// <summary>
	/// Defines the fields over which 'Movies' can be filtered.
	/// </summary
	///
	/// <seealso cref="MovieFilterOrderBy" />
	/// <seealso cref="ModelFilterOrderDirection" />
	public sealed class MovieFilter : ModelFilter<MovieFilterOrderBy, ModelFilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Title' filter.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///  The 'Genre' filter.
		/// </summary>
		public string Genre { get; set; }
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
		/// By 'Title'.
		/// </summary>
		Title = 1,
		/// <summary>
		/// By 'Genre'.
		/// </summary>
		Genre = 2,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		CreatedAt = 3,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		UpdatedAt = 4
	}
}