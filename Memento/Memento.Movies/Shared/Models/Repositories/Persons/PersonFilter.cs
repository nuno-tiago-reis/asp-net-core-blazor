using Memento.Shared.Models.Repositories;
using System;

namespace Memento.Movies.Shared.Models.Repositories.Persons
{
	/// <summary>
	/// Defines the fields over which 'Persons' can be filtered.
	/// </summary
	///
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class PersonFilter : ModelFilter<PersonFilterOrderBy, FilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///  The 'Biography' filter.
		/// </summary>
		public string Biography { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born after).
		/// </summary>
		public DateTime? BornAfter { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born before).
		/// </summary>
		public DateTime? BornBefore { get; set; }
		#endregion
	}

	/// <summary>
	/// Defines the fields over which 'Persons' can be ordered by.
	/// </summary>
	public enum PersonFilterOrderBy
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