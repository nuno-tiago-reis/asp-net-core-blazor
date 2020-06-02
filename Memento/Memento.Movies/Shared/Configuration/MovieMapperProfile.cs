using AutoMapper;
using Memento.Movies.Shared.Models.Identity;
using Memento.Movies.Shared.Models.Identity.Repositories;
using Memento.Movies.Shared.Models.Identity.Contracts.Roles;
using Memento.Movies.Shared.Models.Identity.Contracts.Users;
using Memento.Movies.Shared.Models.Identity.Repositories.Roles;
using Memento.Movies.Shared.Models.Identity.Repositories.Users;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories;
using Memento.Movies.Shared.Models.Movies.Contracts.Genres;
using Memento.Movies.Shared.Models.Movies.Contracts.Movies;
using Memento.Movies.Shared.Models.Movies.Contracts.Persons;
using Memento.Movies.Shared.Models.Movies.Repositories.Genres;
using Memento.Movies.Shared.Models.Movies.Repositories.Movies;
using Memento.Movies.Shared.Models.Movies.Repositories.Persons;
using Memento.Shared.Configuration;
using Memento.Shared.Models.Pagination;
using System;

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
		public MovieMapperProfile()
		{
			this.CreateMappings();
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void CreateMappings()
		{
			base.CreateMappings();

			this.CreateIdentityMappings();
			this.CreateMovieMappings();
		}


		/// <summary>
		/// Creates the mappings related to the <seealso cref="IdentityContext"/>.
		/// </summary>
		private void CreateIdentityMappings()
		{
			base.CreateMappings();

			#region [Contracts: Roles]
			// Roles: Model => Contract
			this.CreateMap<Role, RoleDetailContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.RoleClaims))
				.ForMember(contract => contract.Users, expression => expression.MapFrom(model => model.RoleUsers));
			// Roles: Model => Contract
			this.CreateMap<Role, RoleFormContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.RoleClaims))
				.ForMember(contract => contract.Users, expression => expression.MapFrom(model => model.RoleUsers));
			// Roles: Model => Contract
			this.CreateMap<Role, RoleListContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.RoleClaims))
				.ForMember(contract => contract.Users, expression => expression.MapFrom(model => model.RoleUsers));

			// Roles: Contract => Model
			this.CreateMap<RoleFormContract, Role>()
				.ForMember(model => model.RoleClaims, expression => expression.MapFrom(contract => contract.Claims))
				.ForMember(model => model.RoleUsers, expression => expression.MapFrom(contract => contract.Users));

			// Roles: Contract => Contract
			this.CreateMap<RoleDetailContract, RoleFormContract>();
			// Roles: Contract => Contract
			this.CreateMap<RoleListContract, RoleFormContract>();
			#endregion

			#region [Contracts: RoleClaims]
			// RoleClaims: Model => Contract
			this.CreateMap<RoleClaim, RoleClaimDetailContract>();
			// RoleClaims: Model => Contract
			this.CreateMap<RoleClaim, RoleClaimFormContract>();
			// RoleClaims: Model => Contract
			this.CreateMap<RoleClaim, RoleClaimListContract>();

			// RoleClaims: Contract => Model
			this.CreateMap<RoleClaimFormContract, RoleClaim>();
			#endregion

			#region [Contracts: RoleUsers]
			// RoleUsers: Model => Contract
			this.CreateMap<UserRole, RoleUserDetailContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<RoleUserDetailContract>(model.User))
				.ForAllOtherMembers(contract => contract.Ignore());
			// RoleUsers: Model => Contract
			this.CreateMap<UserRole, RoleUserFormContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<RoleUserFormContract>(model.User))
				.ForAllOtherMembers(model => model.Ignore());
			// RoleUsers: Model => Contract
			this.CreateMap<UserRole, RoleUserListContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<RoleUserListContract>(model.User))
				.ForAllOtherMembers(model => model.Ignore());

			// RoleUsers: Model => Contract
			this.CreateMap<User, RoleUserDetailContract>();
			// RoleUsers: Model => Contract
			this.CreateMap<User, RoleUserFormContract>();
			// RoleUsers: Model => Contract
			this.CreateMap<User, RoleUserListContract>();

			// RoleUsers: Contract => Model
			this.CreateMap<RoleUserFormContract, UserRole>()
				.ForMember(model => model.UserId, expression => expression.MapFrom(contract => contract.Id))
				.ForAllOtherMembers(model => model.Ignore());

			// RoleUsers: Contract => Contract
			this.CreateMap<RoleUserDetailContract, RoleUserFormContract>();
			// RoleUsers: Contract => Contract
			this.CreateMap<RoleUserListContract, RoleUserFormContract>();

			// RoleUsers: Contract => Contract
			this.CreateMap<UserDetailContract, RoleUserFormContract>();
			// RoleUsers: Contract => Contract
			this.CreateMap<UserListContract, RoleUserFormContract>();

			// UserGenres: Contract => Contract
			this.CreateMap<RoleUserDetailContract, UserListContract>();
			// UserGenres: Contract => Contract
			this.CreateMap<RoleUserListContract, UserListContract>();
			#endregion

			#region [Contracts: Users]
			// Users: Model => Contract
			this.CreateMap<User, UserDetailContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.UserClaims))
				.ForMember(contract => contract.Roles, expression => expression.MapFrom(model => model.UserRoles));
			// Users: Model => Contract
			this.CreateMap<User, UserFormContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.UserClaims))
				.ForMember(contract => contract.Roles, expression => expression.MapFrom(model => model.UserRoles));
			// Users: Model => Contract
			this.CreateMap<User, UserListContract>()
				.ForMember(contract => contract.Claims, expression => expression.MapFrom(model => model.UserClaims))
				.ForMember(contract => contract.Roles, expression => expression.MapFrom(model => model.UserRoles));

			// Users: Contract => Model
			this.CreateMap<UserFormContract, User>()
				.ForMember(model => model.UserClaims, expression => expression.MapFrom(contract => contract.Claims))
				.ForMember(model => model.UserRoles, expression => expression.MapFrom(contract => contract.Roles));

			// Users: Contract => Contract
			this.CreateMap<UserDetailContract, UserFormContract>();
			// Users: Contract => Contract
			this.CreateMap<UserListContract, UserFormContract>();
			#endregion

			#region [Contracts: UserClaims]
			// UserClaims: Model => Contract
			this.CreateMap<UserClaim, UserClaimDetailContract>();
			// UserClaims: Model => Contract
			this.CreateMap<UserClaim, UserClaimFormContract>();
			// UserClaims: Model => Contract
			this.CreateMap<UserClaim, UserClaimListContract>();

			// MovieGenres: Contract => Model
			this.CreateMap<UserClaimFormContract, UserClaim>();
			#endregion

			#region [Contracts: UserRoles]
			// UserRoles: Model => Contract
			this.CreateMap<UserRole, UserRoleDetailContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<UserRoleDetailContract>(model.Role))
				.ForAllOtherMembers(model => model.Ignore());
			// UserRoles: Model => Contract
			this.CreateMap<UserRole, UserRoleFormContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<UserRoleFormContract>(model.Role))
				.ForAllOtherMembers(model => model.Ignore());
			// UserRoles: Model => Contract
			this.CreateMap<UserRole, UserRoleListContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<UserRoleListContract>(model.Role))
				.ForAllOtherMembers(model => model.Ignore());

			// UserRoles: Model => Contract
			this.CreateMap<Role, UserRoleDetailContract>();
			// UserRoles: Model => Contract
			this.CreateMap<Role, UserRoleFormContract>();
			// UserRoles: Model => Contract
			this.CreateMap<Role, UserRoleListContract>();

			// UserRoles: Contract => Model
			this.CreateMap<UserRoleFormContract, UserRole>()
				.ForMember(model => model.RoleId, expression => expression.MapFrom(contract => contract.Id))
				.ForAllOtherMembers(model => model.Ignore());

			// UserRoles: Contract => Contract
			this.CreateMap<UserRoleDetailContract, UserRoleFormContract>();
			// UserRoles: Contract => Contract
			this.CreateMap<UserRoleListContract, UserRoleFormContract>();

			// UserRoles: Contract => Contract
			this.CreateMap<RoleDetailContract, UserRoleFormContract>();
			// UserRoles: Contract => Contract
			this.CreateMap<RoleListContract, UserRoleFormContract>();

			// MovieGenres: Contract => Contract
			this.CreateMap<UserRoleDetailContract, RoleListContract>();
			// MovieGenres: Contract => Contract
			this.CreateMap<UserRoleListContract, RoleListContract>();
			#endregion

			#region [Filters: Roles]
			// Genres: Page => Filter
			this.CreateMap<Page<RoleListContract>, RoleFilter>()
				.ForMember(filter => filter.OrderBy, expression => expression.MapFrom(page => Enum.Parse(typeof(RoleFilterOrderBy), page.OrderBy)))
				.ForMember(filter => filter.OrderDirection, expression => expression.MapFrom(page => Enum.Parse(typeof(RoleFilterOrderDirection), page.OrderDirection)));
			#endregion

			#region [Filters: Users]
			// Genres: Page => Filter
			this.CreateMap<Page<UserListContract>, UserFilter>()
				.ForMember(filter => filter.OrderBy, expression => expression.MapFrom(page => Enum.Parse(typeof(UserFilterOrderBy), page.OrderBy)))
				.ForMember(filter => filter.OrderDirection, expression => expression.MapFrom(page => Enum.Parse(typeof(UserFilterOrderDirection), page.OrderDirection)));
			#endregion
		}

		/// <summary>
		/// Creates the mappings related to the <seealso cref="MovieContext"/>.
		/// </summary>
		private void CreateMovieMappings()
		{
			base.CreateMappings();

			#region [Contracts: Genres]
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreDetailContract>();
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreFormContract>();
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreListContract>();

			// Genres: Contract => Model
			this.CreateMap<GenreFormContract, Genre>();

			// Genres: Contract => Contract
			this.CreateMap<GenreDetailContract, GenreFormContract>();
			// Genres: Contract => Contract
			this.CreateMap<GenreListContract, GenreFormContract>();
			#endregion

			#region [Contracts: Movies]
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieDetailContract>();
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieFormContract>();
			// Movies: Model => Contract
			this.CreateMap<Movie, MovieListContract>();

			// Movies: Contract => Model
			this.CreateMap<MovieFormContract, Movie>();

			// Movies: Contract => Contract
			this.CreateMap<MovieDetailContract, MovieFormContract>();
			// Movies: Contract => Contract
			this.CreateMap<MovieListContract, MovieFormContract>();
			#endregion

			#region [Contracts: MovieGenres]
			// MovieGenres: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreDetailContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MovieGenreDetailContract>(model.Genre))
				.ForAllOtherMembers(model => model.Ignore());
			// MovieGenres: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreFormContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MovieGenreFormContract>(model.Genre))
				.ForAllOtherMembers(model => model.Ignore());
			// MovieGenres: Model => Contract
			this.CreateMap<MovieGenre, MovieGenreListContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MovieGenreListContract>(model.Genre))
				.ForAllOtherMembers(model => model.Ignore());

			// MovieGenres: Model => Contract
			this.CreateMap<Genre, MovieGenreDetailContract>();
			// MovieGenres: Model => Contract
			this.CreateMap<Genre, MovieGenreFormContract>();
			// MovieGenres: Model => Contract
			this.CreateMap<Genre, MovieGenreListContract>();

			// MovieGenres: Contract => Model
			this.CreateMap<MovieGenreFormContract, MovieGenre>()
				.ForMember(model => model.GenreId, expression => expression.MapFrom(contract => contract.Id))
				.ForAllOtherMembers(model => model.Ignore());

			// MovieGenres: Contract => Contract
			this.CreateMap<MovieGenreDetailContract, MovieGenreFormContract>();
			// MovieGenres: Contract => Contract
			this.CreateMap<MovieGenreListContract, MovieGenreFormContract>();

			// MovieGenres: Contract => Contract
			this.CreateMap<GenreDetailContract, MovieGenreFormContract>();
			// MovieGenres: Contract => Contract
			this.CreateMap<GenreListContract, MovieGenreFormContract>();

			// MovieGenres: Contract => Contract
			this.CreateMap<MovieGenreDetailContract, GenreListContract>();
			// MovieGenres: Contract => Contract
			this.CreateMap<MovieGenreListContract, GenreListContract>();
			#endregion

			#region [Contracts: MoviePersons]
			// MoviePersons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonDetailContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MoviePersonDetailContract>(model.Person))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(model => model.Ignore());
			// MoviePersons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonFormContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MoviePersonFormContract>(model.Person))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(model => model.Ignore());
			// MoviePersons: Model => Contract
			this.CreateMap<MoviePerson, MoviePersonListContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<MoviePersonListContract>(model.Person))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(model => model.Ignore());

			// MoviePersons: Model => Contract
			this.CreateMap<Person, MoviePersonDetailContract>();
			// MoviePersons: Model => Contract
			this.CreateMap<Person, MoviePersonFormContract>();
			// MoviePersons: Model => Contract
			this.CreateMap<Person, MoviePersonListContract>();

			// MoviePersons: Contract => Model
			this.CreateMap<MoviePersonFormContract, MoviePerson>()
				.ForMember(model => model.PersonId, expression => expression.MapFrom(contract => contract.Id))
				.ForMember(model => model.Role, expression => expression.MapFrom(contract => contract.Role))
				.ForAllOtherMembers(model => model.Ignore());

			// MoviePersons: Contract => Contract
			this.CreateMap<MoviePersonDetailContract, MoviePersonFormContract>();
			// MoviePersons: Contract => Contract
			this.CreateMap<MoviePersonListContract, MoviePersonFormContract>();

			// MoviePersons: Contract => Contract
			this.CreateMap<PersonDetailContract, MoviePersonFormContract>();
			// MoviePersons: Contract => Contract
			this.CreateMap<PersonListContract, MoviePersonFormContract>();

			// MoviePersons: Contract => Contract
			this.CreateMap<MoviePersonDetailContract, PersonListContract>();
			// MoviePersons: Contract => Contract
			this.CreateMap<MoviePersonListContract, PersonListContract>();
			#endregion

			#region [Contracts: Persons]
			// Persons: Model => Contract
			this.CreateMap<Person, PersonDetailContract>();
			// Persons: Model => Contract
			this.CreateMap<Person, PersonFormContract>();
			// Persons: Model => Contract
			this.CreateMap<Person, PersonListContract>();

			// Persons: Contract => Model
			this.CreateMap<PersonFormContract, Person>();

			// Persons: Contract => Contract
			this.CreateMap<PersonDetailContract, PersonFormContract>();
			// Persons: Contract => Contract
			this.CreateMap<PersonListContract, PersonFormContract>();
			#endregion

			#region [Contracts: PersonMovies]
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieDetailContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<PersonMovieDetailContract>(model.Movie))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(contract => contract.Ignore());
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieFormContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<PersonMovieFormContract>(model.Movie))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(model => model.Ignore());
			// PersonMovies: Model => Contract
			this.CreateMap<MoviePerson, PersonMovieListContract>()
				.ConstructUsing((model, context) => context.Mapper.Map<PersonMovieListContract>(model.Movie))
				.ForMember(contract => contract.Role, expression => expression.MapFrom(model => model.Role))
				.ForAllOtherMembers(model => model.Ignore());

			// PersonMovies: Model => Contract
			this.CreateMap<Movie, PersonMovieDetailContract>();
			// PersonMovies: Model => Contract
			this.CreateMap<Movie, PersonMovieFormContract>();
			// PersonMovies: Model => Contract
			this.CreateMap<Movie, PersonMovieListContract>();

			// PersonMovies: Contract => Model
			this.CreateMap<PersonMovieFormContract, MoviePerson>()
				.ForMember(model => model.MovieId, expression => expression.MapFrom(contract => contract.Id))
				.ForMember(model => model.Role, expression => expression.MapFrom(contract => contract.Role))
				.ForAllOtherMembers(model => model.Ignore());

			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieDetailContract, PersonMovieFormContract>();
			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieListContract, PersonMovieFormContract>();

			// PersonMovies: Contract => Contract
			this.CreateMap<MovieDetailContract, PersonMovieFormContract>();
			// PersonMovies: Contract => Contract
			this.CreateMap<MovieListContract, PersonMovieFormContract>();

			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieDetailContract, MovieListContract>();
			// PersonMovies: Contract => Contract
			this.CreateMap<PersonMovieListContract, MovieListContract>();
			#endregion

			#region [Models: Genres]
			// Genres: Page => Filter
			this.CreateMap<Page<GenreListContract>, GenreFilter>()
				.ForMember(filter => filter.OrderBy, expression => expression.MapFrom(page => Enum.Parse(typeof(GenreFilterOrderBy), page.OrderBy)))
				.ForMember(filter => filter.OrderDirection, expression => expression.MapFrom(page => Enum.Parse(typeof(GenreFilterOrderDirection), page.OrderDirection)));
			#endregion

			#region [Filters: Movies]
			// Genres: Page => Filter
			this.CreateMap<Page<MovieListContract>, MovieFilter>()
				.ForMember(filter => filter.OrderBy, expression => expression.MapFrom(page => Enum.Parse(typeof(MovieFilterOrderBy), page.OrderBy)))
				.ForMember(filter => filter.OrderDirection, expression => expression.MapFrom(page => Enum.Parse(typeof(MovieFilterOrderDirection), page.OrderDirection)));
			#endregion

			#region [Filters: Persons]
			// Genres: Page => Filter
			this.CreateMap<Page<PersonListContract>, PersonFilter>()
				.ForMember(filter => filter.OrderBy, expression => expression.MapFrom(page => Enum.Parse(typeof(PersonFilterOrderBy), page.OrderBy)))
				.ForMember(filter => filter.OrderDirection, expression => expression.MapFrom(page => Enum.Parse(typeof(MovieFilterOrderDirection), page.OrderDirection)));
			#endregion
		}
		#endregion
	}
}