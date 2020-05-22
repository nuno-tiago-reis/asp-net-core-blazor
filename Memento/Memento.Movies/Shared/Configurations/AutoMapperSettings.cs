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
			this.CreateMap<Genre, GenreListContract>();

			// Genres: Contract => Model
			this.CreateMap<GenreCreateContract, Genre>();
			this.CreateMap<GenreUpdateContract, Genre>();
			#endregion

			#region [Contracts: Movies]
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieDetailContract>();
			this.CreateMap<Movie, MovieListContract>()
				.ForMember
				(
					contract => contract.Genres,
					expression => expression.MapFrom(model => model.Genres.Select(genre => genre.Genre.Name))
				)
				.ForMember
				(
					contract => contract.Persons,
					expression => expression.MapFrom(model => model.Persons.Select
					(
						person => new Tuple<string, string>(person.Person.Name, person.PersonRole.ToString()))
					)
				);

			// Movies (Associations): Model => Contract
			this.CreateMap<MovieGenre, MovieGenreContract>()
				.ForMember
				(
					contract => contract,
					expression => expression.MapFrom(model => model.Genre)
				);
			this.CreateMap<MoviePerson, MoviePersonContract>()
				.ForMember
				(
					contract => contract,
					expression => expression.MapFrom(model => model.Person)
				)
				.ForMember
				(
					contract => contract.Role,
					expression => expression.MapFrom(model => model.PersonRole)
				);

			// Movies: Contract => Model
			this.CreateMap<MovieCreateContract, Movie>();
			this.CreateMap<MovieUpdateContract, Movie>();

			// Movies (Associations): Model => Contract
			this.CreateMap<long, MovieGenre>()
				.ForMember
				(
					model => model.GenreId,
					expression => expression.MapFrom(contract => contract)
				);
			this.CreateMap<Tuple<long, MoviePersonRole>, MoviePerson>()
				.ForMember
				(
					model => model.PersonId, expression => expression.MapFrom(contract => contract.Item1)
				)
				.ForMember
				(
					model => model.PersonRole, expression => expression.MapFrom(contract => contract.Item2)
				);
			#endregion

			#region [Contracts: Persons]
			// Persons: Model => Contract
			this.CreateMap<Person, PersonDetailContract>();
			this.CreateMap<Person, PersonListContract>()
				.ForMember
				(
					contract => contract.Movies,
					expression => expression.MapFrom(model => model.Movies.Select
					(
						movie => new Tuple<string, DateTime, string>(movie.Movie.Name, movie.Movie.ReleaseDate, movie.PersonRole.ToString()))
					)
				);

			// Persons (Associations): Model => Contract
			this.CreateMap<MoviePerson, PersonMovieContract>()
				.ForMember
				(
					contract => contract,
					expression => expression.MapFrom(model => model.Movie)
				)
				.ForMember
				(
					contract => contract.Role,
					expression => expression.MapFrom(model => model.PersonRole)
				);

			// Persons: Contract => Model
			this.CreateMap<PersonCreateContract, Person>();
			this.CreateMap<PersonUpdateContract, Person>();

			// Persons (Associations): Model => Contract
			this.CreateMap<Tuple<long, MoviePersonRole>, MoviePerson>()
				.ForMember
				(
					model => model.MovieId,
					expression => expression.MapFrom(contract => contract.Item1)
				)
				.ForMember
				(
					model => model.PersonRole,
					expression => expression.MapFrom(contract => contract.Item2)
				);
			#endregion
		}
		#endregion
	}
}