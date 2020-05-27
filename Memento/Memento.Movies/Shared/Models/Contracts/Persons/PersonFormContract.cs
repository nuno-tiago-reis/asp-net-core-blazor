using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonForm' contract.
	/// </summary>
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
		/// The Person's picture url.
		/// </summary>
		[Required]
		[MaxLength(PersonConfiguration.PICTURE_URL_MAXIMUM_LENGTH)]
		[Display(Name = nameof(SharedResources.PERSON_PICTUREURL), ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

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
		public List<Tuple<long, MoviePersonRole>> Movies { get; set; }
		#endregion
	}
}