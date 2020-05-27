using Memento.Movies.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonList' contract.
	/// </summary>
	public sealed class PersonListContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_ID, ResourceType = typeof(SharedResources))]
		public long Id { get; set; }

		/// <summary>
		/// The Person's name.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The Person's normalized name.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		public string NormalizedName { get; set; }

		/// <summary>
		/// The Person's biography.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_BIOGRAPHY, ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		/// The Person's picture url.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_PICTUREURL, ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Person's birth date.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_BIRTHDATE, ResourceType = typeof(SharedResources))]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The Movies associated with the Person.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_MOVIES, ResourceType = typeof(SharedResources))]
		public List<Tuple<string, DateTime, string>> Movies { get; set; }

		/// <summary>
		/// The Person's created by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_CREATEDBY, ResourceType = typeof(SharedResources))]
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Person's created at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_CREATEDAT, ResourceType = typeof(SharedResources))]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Person's updated by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_UPDATEDBY, ResourceType = typeof(SharedResources))]
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Person's updated at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_UPDATEDAT, ResourceType = typeof(SharedResources))]
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}