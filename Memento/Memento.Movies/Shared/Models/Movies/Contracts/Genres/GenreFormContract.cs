using Memento.Movies.Shared.Models.Movies.Repositories.Genres;
using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Movies.Contracts.Genres
{
	/// <summary>
	/// Implements the 'GenreForm' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class GenreFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's name.
		/// </summary>
		[Required]
		[MaxLength(GenreConfiguration.NAME_MAXIMUM_LENGTH)]
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
		#endregion
	}
}