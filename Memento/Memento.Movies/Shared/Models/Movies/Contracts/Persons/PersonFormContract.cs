using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonForm' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PersonFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's name.
		/// </summary>
		[Required]
		[MaxLength(PersonConfiguration.NAME_MAXIMUM_LENGTH)]
		[Display(Name = nameof(SharedResources.PERSON_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The Person's biography.
		/// </summary>
		[Required]
		[MaxLength(PersonConfiguration.BIOGRAPHY_MAXIMUM_LENGTH)]
		[Display(Name = nameof(SharedResources.PERSON_BIOGRAPHY), ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		/// The Person's picture.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_PICTURE), ResourceType = typeof(SharedResources))]
		public File Picture { get; set; }

		/// <summary>
		/// The Person's birth date.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.PERSON_BIRTHDATE), ResourceType = typeof(SharedResources))]
		public DateTime? BirthDate { get; set; }

		/// <summary>
		/// The Movies associated with the Person.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_MOVIES), ResourceType = typeof(SharedResources))]
		public List<PersonMovieFormContract> Movies { get; set; }
		#endregion
	}
}