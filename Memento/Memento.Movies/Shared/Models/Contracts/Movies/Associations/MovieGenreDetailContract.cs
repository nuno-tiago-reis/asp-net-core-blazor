using Memento.Movies.Shared.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Contracts.Movies
{
	/// <summary>
	/// Implements the 'MovieGenreDetail' contract.
	/// </summary>
	public sealed class MovieGenreDetailContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ID), ResourceType = typeof(SharedResources))]
		public long Id { get; set; }

		/// <summary>
		/// The Genre's name.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }
	
		/// <summary>
		/// The Genre's created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_CREATEDBY), ResourceType = typeof(SharedResources))]
		public long CreatedBy { get; set; }

		/// <summary>
		/// The Genre's created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_CREATEDAT), ResourceType = typeof(SharedResources))]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The Genre's updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public long? UpdatedBy { get; set; }

		/// <summary>
		/// The Genre's updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public DateTime? UpdatedAt { get; set; }
		#endregion
	}
}