using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Repositories.Persons
{
	/// <summary>
	/// Implements the 'Person' model.
	/// </summary>
	public sealed class Person : Model
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_ID, ResourceType = typeof(SharedResources))]
		public override long Id { get; set; }

		/// <summary>
		/// The name.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The normalized name.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_NAME, ResourceType = typeof(SharedResources))]
		public string NormalizedName { get; set; }

		/// <summary>
		/// The biography.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_BIOGRAPHY, ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		/// The picture url.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_PICTUREURL, ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The birth date.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_BIRTHDATE, ResourceType = typeof(SharedResources))]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The movies associated with the person.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_MOVIES, ResourceType = typeof(SharedResources))]
		public List<MoviePerson> Movies { get; set; }

		/// <summary>
		/// The created by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_CREATEDBY, ResourceType = typeof(SharedResources))]
		public override long CreatedBy { get; set; }

		/// <summary>
		/// The created at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_CREATEDAT, ResourceType = typeof(SharedResources))]
		public override DateTime CreatedAt { get; set; }

		/// <summary>
		/// The updated by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_UPDATEDBY, ResourceType = typeof(SharedResources))]
		public override long? UpdatedBy { get; set; }

		/// <summary>
		/// The updated at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Person.Model.PERSON_UPDATEDAT, ResourceType = typeof(SharedResources))]
		public override DateTime? UpdatedAt { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override bool Equals(object @object)
		{
			if (@object is Person person)
			{
				return this.Id == person.Id;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}