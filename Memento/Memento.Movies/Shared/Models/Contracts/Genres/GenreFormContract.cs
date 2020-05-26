using Memento.Movies.Shared.Models.Repositories.Genres;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Genres
{
	/// <summary>
	/// Implements the 'GenreForm' contract.
	/// </summary>
	public sealed class GenreFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's name.
		/// </summary>
		[Required]
		[MaxLength(GenreConfiguration.NAME_MAXIMUM_LENGTH)]
		public string Name { get; set; }
		#endregion
	}
}