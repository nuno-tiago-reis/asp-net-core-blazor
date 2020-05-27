using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieForm' contract.
	/// </summary>
	public sealed class MovieFormContract
	{
		#region [Properties]
		/// <summary>
		/// The Movie's name.
		/// </summary>
		[Required]
		[MaxLength(MovieConfiguration.NAME_MAXIMUM_LENGTH)]
		[Display(Name = SharedResources.Movie.Model.MOVIE_NAME, ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The Movie's summary.
		/// </summary>
		[Required]
		[MaxLength(MovieConfiguration.SUMMARY_MAXIMUM_LENGTH)]
		[Display(Name = SharedResources.Movie.Model.MOVIE_SUMMARY, ResourceType = typeof(SharedResources))]
		public string Summary { get; set; }

		/// <summary>
		/// The Movie's picture url.
		/// </summary>
		[Required]
		[MaxLength(MovieConfiguration.PICTURE_URL_MAXIMUM_LENGTH)]
		[Display(Name = SharedResources.Movie.Model.MOVIE_PICTUREURL, ResourceType = typeof(SharedResources))]
		public string PictureUrl { get; set; }

		/// <summary>
		/// The Movie's trailer url.
		/// </summary>
		[Required]
		[MaxLength(MovieConfiguration.TRAILER_URL_MAXIMUM_LENGTH)]
		[Display(Name = SharedResources.Movie.Model.MOVIE_TRAILERURL, ResourceType = typeof(SharedResources))]
		public string TrailerUrl { get; set; }

		/// <summary>
		/// The Movie's release date.
		/// </summary>
		[Required]
		[Display(Name = SharedResources.Movie.Model.MOVIE_RELEASEDATE, ResourceType = typeof(SharedResources))]
		public DateTime? ReleaseDate { get; set; }

		/// <summary>
		/// Whether the Movie is in theaters.
		/// </summary>
		[Required]
		[Display(Name = SharedResources.Movie.Model.MOVIE_INTHEATERS, ResourceType = typeof(SharedResources))]
		public bool? InTheaters { get; set; }

		/// <summary>
		/// The Genres associated with the Movie.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_GENRES, ResourceType = typeof(SharedResources))]
		public List<long> Genres { get; set; }

		/// <summary>
		/// The Persons associated with the Movie.
		/// </summary>
		[Display(Name = SharedResources.Movie.Model.MOVIE_PERSONS, ResourceType = typeof(SharedResources))]
		public List<Tuple<long, MoviePersonRole>> Persons { get; set; }
		#endregion
	}
}