using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models
{
	/// <summary>
	/// Implements the 'MoviePerson' model.
	/// </summary>
	public sealed class MoviePerson : Model
	{
		#region [Properties]
		/// <summary>
		/// The identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_ID), ResourceType = typeof(SharedResources))]
		public override long Id { get; set; }

		/// <summary>
		/// The 'Movie' identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_MOVIEID), ResourceType = typeof(SharedResources))]
		public long MovieId { get; set; }

		/// <summary>
		/// The 'Movie' navigation property.
		/// </summary>
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_MOVIE), ResourceType = typeof(SharedResources))]
		public Movie Movie { get; set; }

		/// <summary>
		/// The 'Person' identifier.
		/// </summary>
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_PERSONID), ResourceType = typeof(SharedResources))]
		public long PersonId { get; set; }

		/// <summary>
		/// The 'Person' navigation property.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_PERSON), ResourceType = typeof(SharedResources))]
		public Person Person { get; set; }

		/// <summary>
		/// The persons role.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_PERSONROLE), ResourceType = typeof(SharedResources))]
		public MoviePersonRole PersonRole { get; set; }

		/// <summary>
		/// The created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_CREATEDBY), ResourceType = typeof(SharedResources))]
		public override long CreatedBy { get; set; }

		/// <summary>
		/// The created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_CREATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime CreatedAt { get; set; }

		/// <summary>
		/// The updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public override long? UpdatedBy { get; set; }

		/// <summary>
		/// The updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime? UpdatedAt { get; set; }
		#endregion
	}

	/// <summary>
	/// Defines the roles a person can have in a movie.
	/// </summary>
	public enum MoviePersonRole
	{
		/// <summary>
		/// The person is an actor.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSONROLE_ACTOR), ResourceType = typeof(SharedResources))]
		Actor = 0,
		/// <summary>
		/// The person is a director
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSONROLE_DIRECTOR), ResourceType = typeof(SharedResources))]
		Director = 1,
		/// <summary>
		/// The person is a writer.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSONROLE_WRITER), ResourceType = typeof(SharedResources))]
		Writer = 2
	}
}