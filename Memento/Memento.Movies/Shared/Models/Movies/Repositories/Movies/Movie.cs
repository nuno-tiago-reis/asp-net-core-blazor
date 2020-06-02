using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Movies
{
	/// <summary>
	/// Implements the 'Movie' model.
	/// </summary>
	public sealed class Movie : Model
	{
		#region [Properties]
		/// <summary>
		/// The identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_ID), ResourceType = typeof(SharedResources))]
		public override long Id { get; set; }

		/// <summary>
		/// The name.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The normalized name.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_NAME), ResourceType = typeof(SharedResources))]
		public string NormalizedName { get; set; }

		/// <summary>
		/// The summary.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_SUMMARY), ResourceType = typeof(SharedResources))]
		public string Summary { get; set; }

		/// <summary>
		/// The picture url.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_PICTUREURL), ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The trailer url.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_TRAILERURL), ResourceType = typeof(SharedResources))]
		public string TrailerUrl { get; set; }

		/// <summary>
		/// The release date.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_RELEASEDATE), ResourceType = typeof(SharedResources))]
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Whether the movie is in theaters.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_INTHEATERS), ResourceType = typeof(SharedResources))]
		public bool InTheaters { get; set; }

		/// <summary>
		/// The genres associated with the movie.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_GENRES), ResourceType = typeof(SharedResources))]
		public List<MovieGenre> Genres { get; set; }

		/// <summary>
		/// The persons associated with the movie.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_PERSONS), ResourceType = typeof(SharedResources))]
		public List<MoviePerson> Persons { get; set; }

		/// <summary>
		/// The created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_CREATEDBY), ResourceType = typeof(SharedResources))]
		public override long CreatedBy { get; set; }

		/// <summary>
		/// The created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_CREATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime CreatedAt { get; set; }

		/// <summary>
		/// The updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public override long? UpdatedBy { get; set; }

		/// <summary>
		/// The updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.MOVIE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime? UpdatedAt { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override bool Equals(object @object)
		{
			if (@object is Movie movie)
			{
				return this.Id == movie.Id;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}