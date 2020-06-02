using Memento.Movies.Shared.Models.Movies.Repositories.Genres;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Implements the 'MovieGenre' model.
	/// </summary>
	public sealed class MovieGenre : Model
	{
		#region [Properties]
		/// <summary>
		/// The identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_ID), ResourceType = typeof(SharedResources))]
		public override long Id { get; set; }

		/// <summary>
		/// The 'Movie' identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_MOVIEID), ResourceType = typeof(SharedResources))]
		public long MovieId { get; set; }

		/// <summary>
		/// The 'Movie' navigation property.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_MOVIE), ResourceType = typeof(SharedResources))]
		public Movie Movie { get; set; }

		/// <summary>
		/// The 'Genre' identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_GENREID), ResourceType = typeof(SharedResources))]
		public long GenreId { get; set; }

		/// <summary>
		/// The 'Genre' navigation property.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_GENRE), ResourceType = typeof(SharedResources))]
		public Genre Genre { get; set; }

		/// <summary>
		/// The created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_CREATEDBY), ResourceType = typeof(SharedResources))]
		public override long CreatedBy { get; set; }

		/// <summary>
		/// The created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_CREATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime CreatedAt { get; set; }

		/// <summary>
		/// The updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public override long? UpdatedBy { get; set; }

		/// <summary>
		/// The updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIEGENRE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime? UpdatedAt { get; set; }
		#endregion
	}
}