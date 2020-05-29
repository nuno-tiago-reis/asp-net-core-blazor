using Memento.Movies.Shared.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonMovieForm' contract.
	/// </summary>
	public sealed class PersonMovieFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Person's role in the Movie.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEPERSON_ROLE), ResourceType = typeof(SharedResources))]
		public MoviePersonRole Role { get; set; }

		/// <summary>
		/// The Movie's identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_ID), ResourceType = typeof(SharedResources))]
		public long Id { get; set; }
		#endregion
	}
}