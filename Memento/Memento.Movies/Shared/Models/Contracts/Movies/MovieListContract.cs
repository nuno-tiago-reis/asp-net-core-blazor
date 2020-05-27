using Memento.Movies.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieList' contract.
	/// </summary>
	public sealed class MovieListContract
	{
		#region [Properties]
		/// <summary>
		/// The Movie's identifier.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_ID, ResourceType = typeof(SharedResources))]
		public long Id { get; set; }

		/// <summary>
		/// The Movie's name.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_NAME, ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The Movie's normalized name.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_NAME, ResourceType = typeof(SharedResources))]
		public string NormalizedName { get; set; }

		/// <summary>
		/// The Movie's summary.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_SUMMARY, ResourceType = typeof(SharedResources))]
		public string Summary { get; set; }

		/// <summary>
		/// The Movie's picture url.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_PICTUREURL, ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Movie's trailer url.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_TRAILERURL, ResourceType = typeof(SharedResources))]
		public string TrailerUrl { get; set; }

		/// <summary>
		/// The Movie's release date.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_RELEASEDATE, ResourceType = typeof(SharedResources))]
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Whether the Movie is in theaters.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_INTHEATERS, ResourceType = typeof(SharedResources))]
		public bool InTheaters { get; set; }

		/// <summary>
		/// The Genres associated with the Movie.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_GENRES, ResourceType = typeof(SharedResources))]
		public List<string> Genres { get; set; }

		/// <summary>
		/// The Persons associated with the Movie.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_PERSONS, ResourceType = typeof(SharedResources))]
		public List<Tuple<string, string>> Persons { get; set; }

		/// <summary>
		/// The Movie's created by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_CREATEDBY, ResourceType = typeof(SharedResources))]
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Movie's created at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_CREATEDAT, ResourceType = typeof(SharedResources))]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Movie's updated by user identifier.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_UPDATEDBY, ResourceType = typeof(SharedResources))]
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Movie's updated at timestamp.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_UPDATEDAT, ResourceType = typeof(SharedResources))]
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}