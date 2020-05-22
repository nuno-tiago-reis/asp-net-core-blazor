using Memento.Movies.Shared.Models;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonCreate' contract.
	/// </summary>
	public sealed class PersonCreateContract
	{
		#region [Properties]
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