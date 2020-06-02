using Memento.Movies.Shared.Models.Movies.Repositories;
using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MoviePersonForm' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class MoviePersonFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's role in the Movie.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.MOVIEPERSON_ROLE), ResourceType = typeof(SharedResources))]
		public MoviePersonRole? Role { get; set; }

		/// <summary>
		/// The Person's identifier.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.PERSON_ID), ResourceType = typeof(SharedResources))]
		public long? Id { get; set; }
		#endregion
	}
}