using AutoMapper;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Shared.Configuration;
using System;
using System.Linq;

namespace Memento.Movies.Shared.Configuration
{
	/// <summary>
	/// Implements the 'MovieMapper' profile.
	/// </summary>
	/// 
	/// <seealso cref="Profile" />
	public sealed class MovieMapperProfile : MementoMapperProfile
	{
		#region [Constructor]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieMapperProfile"/> class.
		/// </summary>
		public MovieMapperProfile() : base()
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

			#region [Contracts: Movies]
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieDetailContract>();
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieListContract>();

			// Movies: Contract => Model
			this.CreateMap<MovieFormContract, Movie>();
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieFormContract>();

			// Movies: Contract => Contract
			this.CreateMap<MovieFormContract, MovieDetailContract>();
			// Movies: Contract => Contract
			this.CreateMap<MovieDetailContract, MovieFormContract>();
			#endregion

			#region [Contracts: Persons]
			// Persons: Model => Contract
			this.CreateMap<Person, PersonDetailContract>();
			// Persons: Model => Contract
			this.CreateMap<Person, PersonListContract>();

			// Persons: Contract => Model
			this.CreateMap<PersonFormContract, Person>();
			// Persons: Model => Contract
			this.CreateMap<Person, PersonFormContract>();

			// Persons: Contract => Contract
			this.CreateMap<PersonFormContract, PersonDetailContract>();
			// Persons: Contract => Contract
			this.CreateMap<PersonDetailContract, PersonFormContract>();
			#endregion
		}
		#endregion
	}
}