﻿using Memento.Movies.Shared.Models.Movies.Repositories;
using Memento.Movies.Shared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MoviePersonDetail' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class MoviePersonDetailContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's role in the Movie.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_ROLE), ResourceType = typeof(SharedResources))]
		public MoviePersonRole Role { get; set; }

		/// <summary>
		/// The Person's identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_ID), ResourceType = typeof(SharedResources))]
		public long Id { get; set; }

		/// <summary>
		/// The Person's name.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The Person's biography.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_BIOGRAPHY), ResourceType = typeof(SharedResources))]
		public string Biography { get; set; }

		/// <summary>
		/// The Person's picture url.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_PICTUREURL), ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Person's created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_CREATEDBY), ResourceType = typeof(SharedResources))]
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Person's created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_CREATEDAT), ResourceType = typeof(SharedResources))]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Person's updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Person's updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}