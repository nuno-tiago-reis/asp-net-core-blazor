using Memento.Movies.Shared.Models.Movies.Repositories;
using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonMovieForm' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PersonMovieFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's role in the Movie.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.MOVIEPERSON_ROLE), ResourceType = typeof(SharedResources))]
		public MoviePersonRole? Role { get; set; }

		/// <summary>
		/// The Movie's identifier.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.MOVIE_ID), ResourceType = typeof(SharedResources))]
		public long? Id { get; set; }
		#endregion
	}
}