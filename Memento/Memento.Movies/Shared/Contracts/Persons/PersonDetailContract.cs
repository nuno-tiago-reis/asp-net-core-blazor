﻿using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonDetail' contract.
	/// </summary>
	public sealed class PersonDetailContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's identifier.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The Person's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Person's normalized name.
		/// </summary>
		public string NormalizedName { get; set; }

		/// <summary>
		/// The Person's biography.
		/// </summary>
		public string Biography { get; set; }

		/// <summary>
		/// The Person's picture url.
		/// </summary>
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Person's birth date.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The Movies associated with the Person.
		/// </summary>
		public List<PersonMovieContract> Movies { get; set; }

		/// <summary>
		/// The Person's created by user identifier.
		/// </summary>
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Person's created at timestamp.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Person's updated by user identifier.
		/// </summary>
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Person's updated at timestamp.
		/// </summary>
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}