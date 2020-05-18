using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Persons
{
	/// <summary>
	/// Defines the fields over which 'Persons' can be filtered.
	/// </summary>
	///
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="PersonFilterOrderDirection" />
	public sealed class PersonFilter : ModelFilter<PersonFilterOrderBy, PersonFilterOrderDirection>
	{
		#region [Properties]
		/// <summary>
		///  The 'Name' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		///  The 'Biography' filter.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_BIOGRAPHY), ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born after).
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_BORNAFTER), ResourceType = typeof(SharedResources))]
		public DateTime? BornAfter { get; set; }

		/// <summary>
		///  The 'BirthDate' filter (born before).
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_BORNBEFORE), ResourceType = typeof(SharedResources))]
		public DateTime? BornBefore { get; set; }
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

			// Biography
			if (query.TryGetValue(nameof(this.Biography), out var biography))
			{
				this.Biography = biography;
			}

			// BornAfter
			if (query.TryGetValue(nameof(this.BornAfter), out var bornAfterQuery))
			{
				if (DateTime.TryParse(bornAfterQuery, out var bornAfter))
				{
					this.BornAfter = bornAfter;
				}
			}

			// BornBefore
			if (query.TryGetValue(nameof(this.BornBefore), out var bornBeforeQuery))
			{
				if (DateTime.TryParse(bornBeforeQuery, out var bornBefore))
				{
					this.BornBefore = bornBefore;
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

			// Biography
			if (!string.IsNullOrWhiteSpace(this.Biography))
			{
				query.Add(nameof(this.Biography), this.Biography);
			}

			// BornAfter
			if (this.BornAfter != null)
			{
				query.Add(nameof(this.BornAfter), this.BornAfter.Value.ToShortDateString());
			}

			// BornBefore
			if (this.BornBefore != null)
			{
				query.Add(nameof(this.BornBefore), this.BornBefore.Value.ToShortDateString());
			}
		}
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
		[Display(Name = nameof(SharedResources.PERSON_ID), ResourceType = typeof(SharedResources))]
		Id = 0,
		/// <summary>
		/// By 'Name'.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_NAME), ResourceType = typeof(SharedResources))]
		Name = 1,
		/// <summary>
		/// By 'BirthDate'.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_BIRTHDATE), ResourceType = typeof(SharedResources))]
		BirthDate = 2,
		/// <summary>
		/// By 'CreatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_CREATEDAT), ResourceType = typeof(SharedResources))]
		CreatedAt = 3,
		/// <summary>
		/// By 'UpdatedAt'.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_UPDATEDAT), ResourceType = typeof(SharedResources))]
		UpdatedAt = 4
	}

	/// <summary>
	/// Defines the direction over which 'Persons' can be ordered by.
	/// </summary>
	public enum PersonFilterOrderDirection
	{
		/// <summary>
		/// In 'Ascending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_ORDER_ASCENDING), ResourceType = typeof(SharedResources))]
		Ascending = 0,
		/// <summary>
		/// In 'Descending' order.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_ORDER_DESCENDING), ResourceType = typeof(SharedResources))]
		Descending = 1
	}
}