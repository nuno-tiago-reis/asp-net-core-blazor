using Memento.Shared.Models.Repository;
using System.Collections.Generic;

namespace Memento.Movies.Shared.Models.Genres
{
	/// <summary>
	/// Implements the 'Genre' model.
	/// </summary>
	public sealed class Genre : Model
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
		/// The movies associated with the genre.
		/// </summary>
		public List<MovieGenre> Movies { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override bool Equals(object @object)
		{
			if (@object is Genre genre)
			{
				return this.Id == genre.Id;
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