using AutoMapper;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Contracts.Movies;
using Memento.Movies.Shared.Contracts.Persons;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Genres;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Pagination;
using System;
using System.Linq;

namespace Memento.Movies.Shared.Configuration
{
	/// <summary>
	/// Implements the 'MovieMapper' settings.
	/// </summary>
	/// 
	/// <seealso cref="Profile" />
	public sealed class MovieMapperSettings : AutoMapperSettings
	{
		#region [Constructor]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieMapperSettings"/> class.
		/// </summary>
		public MovieMapperSettings() : base()
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void CreateMappings()
		{
			base.CreateMappings();

			#region [Contracts: Genres]
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreDetailContract>();

			// Genres: Model => Contract
			this.CreateMap<Genre, GenreListContract>();

			// Genres: Contract => Model
			this.CreateMap<GenreFormContract, Genre>();

			// Genres: Model => Contract
			this.CreateMap<Genre, GenreFormContract>();

			// Genres: Contract => Contract
			this.CreateMap<GenreFormContract, GenreDetailContract>();

			// Genres: Contract => Contract
			this.CreateMap<GenreDetailContract, GenreFormContract>();
			#endregion
		}
		#endregion
	}
}