using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieGenreForm' contract.
	/// </summary>
	public sealed class MovieGenreFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ID), ResourceType = typeof(SharedResources))]
		public long Id { get; set; }
		#endregion
	}
}