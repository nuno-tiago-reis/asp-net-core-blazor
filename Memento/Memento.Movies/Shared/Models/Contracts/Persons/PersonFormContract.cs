using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Repositories.Persons;
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
		public string Name { get; set; }

		/// <summary>
		/// The Person's biography.
		/// </summary>
		[Required]
		[MaxLength(PersonConfiguration.BIOGRAPHY_MAXIMUM_LENGTH)]
		public string Biography { get; set; }

		/// <summary>
		/// The Person's picture url.
		/// </summary>
		[Required]
		[MaxLength(PersonConfiguration.PICTURE_URL_MAXIMUM_LENGTH)]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Person's birth date.
		/// </summary>
		[Required]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The Movies associated with the Person.
		/// </summary>
		public List<Tuple<long, MoviePersonRole>> Movies { get; set; }
		#endregion
	}
}