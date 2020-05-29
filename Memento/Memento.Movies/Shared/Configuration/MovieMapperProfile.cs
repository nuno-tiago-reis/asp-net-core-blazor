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

			#region [Contracts: MovieGenres]
			// Persons: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreDetailContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Genre));
			// Persons: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreFormContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Genre));
			// Persons: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreListContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Genre));

			// Persons: Contract => Model
			this.CreateMap<MovieGenreFormContract, MovieGenre>()
				.ForPath(model => model.GenreId, expression => expression.MapFrom(contract => contract.Id));

			// Persons: Contract => Contract
			this.CreateMap<MovieGenreFormContract, MovieGenreDetailContract>();
			// Persons: Contract => Contract
			this.CreateMap<MovieGenreDetailContract, MovieGenreFormContract>();
			#endregion

			#region [Contracts: MoviePersons]
			// Persons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonDetailContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Person))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));
			// Persons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonFormContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Person))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));
			// Persons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonListContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Person))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));

			// Persons: Contract => Model
			this.CreateMap<MoviePersonFormContract, MoviePerson>()
				.ForPath(model => model.PersonId, expression => expression.MapFrom(contract => contract.Id))
				.ForPath(model => model.Role, expression => expression.MapFrom(contract => contract.Role));

			// Persons: Contract => Contract
			this.CreateMap<MoviePersonFormContract, MoviePersonDetailContract>();
			// Persons: Contract => Contract
			this.CreateMap<MoviePersonDetailContract, MoviePersonFormContract>();
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

			#region [Contracts: PersonMovies]
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieDetailContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Movie))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieFormContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Movie))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieListContract>()
				.ForPath(contract => contract, expression => expression.MapFrom(model => model.Movie))
				.ForPath(contract => contract.Role, expression => expression.MapFrom(model => model.Role));

			// PersonMovies: Contract => Model
			this.CreateMap<PersonMovieFormContract, MoviePerson>()
				.ForPath(model => model.MovieId, expression => expression.MapFrom(contract => contract.Id))
				.ForPath(model => model.Role, expression => expression.MapFrom(contract => contract.Role));

			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieFormContract, PersonMovieDetailContract>();
			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieDetailContract, PersonMovieFormContract>();
			#endregion
		}
		#endregion
	}
}