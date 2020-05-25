using AutoMapper;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Contracts.Movies;
using Memento.Movies.Shared.Contracts.Persons;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Genres;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Persons;
using System;
using System.Linq;

namespace Memento.Movies.Shared.Configurations
{
	/// <summary>
	/// Implements the 'AutoMapper' settings.
	/// </summary>
	/// 
	/// <seealso cref="Profile" />
	public sealed class AutoMapperSettings : Profile
	{
		#region [Constructor]
		/// <summary>
		/// Initializes a new instance of the <see cref="AutoMapperSettings"/> class.
		/// </summary>
		public AutoMapperSettings()
		{
			this.MapContracts();
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Maps the contracts into database models and vice-versa.
		/// </summary>
		private void MapContracts()
		{
			#region [Contracts: Genres]
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreDetailContract>();

			// Genres: Model => Contract
			this.CreateMap<Genre, GenreListContract>();

			// Genres: Contract => Model
			this.CreateMap<GenreFormContract, Genre>();

			// Genres: Contract => Contract
			this.CreateMap<GenreDetailContract, GenreFormContract>();
			#endregion
		}
		#endregion
	}
}