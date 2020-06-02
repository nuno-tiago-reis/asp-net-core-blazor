using Memento.Movies.Shared.Resources;
using Memento.Shared.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Genres
{
	/// <summary>
	/// Implements the 'Genre' model.
	/// </summary>
	public sealed class Genre : Model
	{
		#region [Properties]
		/// <summary>
		/// The identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_ID), ResourceType = typeof(SharedResources))]
		public override long Id { get; set; }

		/// <summary>
		/// The name.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string Name { get; set; }

		/// <summary>
		/// The normalized name.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_NAME), ResourceType = typeof(SharedResources))]
		public string NormalizedName { get; set; }

		/// <summary>
		/// The movies associated with the genre.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_MOVIES), ResourceType = typeof(SharedResources))]
		public List<MovieGenre> Movies { get; set; }

		/// <summary>
		/// The created by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_CREATEDBY), ResourceType = typeof(SharedResources))]
		public override long CreatedBy { get; set; }

		/// <summary>
		/// The created at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_CREATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime CreatedAt { get; set; }

		/// <summary>
		/// The updated by user identifier.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_UPDATEDBY), ResourceType = typeof(SharedResources))]
		public override long? UpdatedBy { get; set; }

		/// <summary>
		/// The updated at timestamp.
		/// </summary>
		[Display(Name = nameof(SharedResources.GENRE_UPDATEDAT), ResourceType = typeof(SharedResources))]
		public override DateTime? UpdatedAt { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override bool Equals(object @object)
		{
			if (@object is Genre genre)
			{
				return this.Id == genre.Id;
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