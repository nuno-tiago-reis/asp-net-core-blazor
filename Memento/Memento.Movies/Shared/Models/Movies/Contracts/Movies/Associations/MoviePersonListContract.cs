using Memento.Movies.Shared.Models.Movies.Repositories;
using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MoviePersonList' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class MoviePersonListContract
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

		/// <summary>
		/// The Person's name.
		/// </summary>
		[Display(Name = nameof(SharedResources.PERSON_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
		#endregion
	}
}