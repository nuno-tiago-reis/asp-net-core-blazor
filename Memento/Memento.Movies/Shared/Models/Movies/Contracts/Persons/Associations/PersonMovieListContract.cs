using Memento.Movies.Shared.Resources;
using Memento.Movies.Shared.Models.Movies.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Persons
{
	/// <summary>
	/// Implements the 'PersonMovieList' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PersonMovieListContract
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

		/// <summary>
		/// The Movie's name.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
		#endregion
	}
}