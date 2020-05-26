using Memento.Shared.Models.Repositories;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Models.Repositories.Persons
{
	/// <summary>
	/// Implements the 'Person' model.
	/// </summary>
	public sealed class Person : Model
	{
		#region [Properties]
		/// <summary>
		/// The name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The normalized name.
		/// </summary>
		public string NormalizedName { get; set; }

		/// <summary>
		/// The biography.
		/// </summary>
		public string Biography { get; set; }

		/// <summary>
		/// The picture url.
		/// </summary>
		public string PictureUrl { get; set; }

		/// <summary>
		/// The birth date.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The movies associated with the person.
		/// </summary>
		public List<MoviePerson> Movies { get; set; }
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