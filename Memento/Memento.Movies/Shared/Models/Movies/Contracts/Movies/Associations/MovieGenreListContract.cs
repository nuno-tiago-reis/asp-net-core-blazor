using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieGenreList' contract.
	/// </summary>
	public sealed class MovieGenreListContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ID), ResourceType = typeof(SharedResources))]
		public long Id { get; set; }

		/// <summary>
		/// The Genre's name.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
		#endregion
	}
}