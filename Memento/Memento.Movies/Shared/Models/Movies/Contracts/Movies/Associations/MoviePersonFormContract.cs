using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MoviePersonForm' contract.
	/// </summary>
	public sealed class MoviePersonFormContract
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
		#endregion
	}
}