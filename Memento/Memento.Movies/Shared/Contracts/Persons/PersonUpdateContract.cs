using Memento.Movies.Shared.Database.Models;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonUpdate' contract.
	/// </summary>
	public sealed class PersonUpdateContract
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
		public List<Tuple<long, MoviePersonRole>> Movies { get; set; }
		#endregion
	}
}