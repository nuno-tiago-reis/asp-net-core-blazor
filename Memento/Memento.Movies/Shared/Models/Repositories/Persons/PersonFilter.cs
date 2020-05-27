using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

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
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		///  The 'Biography' filter.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_BIOGRAPHY, ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born after).
		/// </summary>
		[Display(Name = SharedResources.Person.Model.MOVIE_BORNAFTER, ResourceType = typeof(SharedResources))]
		public DateTime? BornAfter { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born before).
		/// </summary>
		[Display(Name = SharedResources.Person.Model.MOVIE_BORNBEFORE, ResourceType = typeof(SharedResources))]
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
		[Display(Name = SharedResources.Person.Model.PERSON_ID, ResourceType = typeof(SharedResources))]
		Id = 0,
		/// <summary>
		/// By 'Name'.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		Name = 1,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_CREATEDAT, ResourceType = typeof(SharedResources))]
		CreatedAt = 2,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_UPDATEDAT, ResourceType = typeof(SharedResources))]
		UpdatedAt = 3
	}
}