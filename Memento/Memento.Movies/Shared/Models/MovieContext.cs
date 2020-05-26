using Memento.Movies.Shared.Models.Genres;
using Memento.Movies.Shared.Models.Movies;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models
{
	/// <summary>
	/// Implements the movie database context.
	/// </summary>
	///
	/// <seealso cref="DbContext"/>
	public sealed class MovieContext : ModelContext
	{
		#region [Properties] Models
		/// <summary>
		/// Gets or sets the 'Genre' database set.
		/// </summary>
		public DbSet<Genre> Genres { get; set; }

		/// <summary>
		/// Gets or sets the 'Movie' database set.
		/// </summary>
		public DbSet<Movie> Movies { get; set; }

		/// <summary>
		/// Gets or sets the 'Person' database set.
		/// </summary>
		public DbSet<Person> Persons { get; set; }
		#endregion

		#region [Properties] Model Associations
		/// <summary>
		/// Gets or sets the 'MovieGenre' database set.
		/// </summary>
		public DbSet<MovieGenre> MovieGenres { get; set; }

		/// <summary>
		/// Gets or sets the 'MoviePerson' database set.
		/// </summary>
		public DbSet<MoviePerson> MoviePersons { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieContext"/> class.
		/// </summary>
		/// 
		/// <param name="options">The options.</param>
		public MovieContext(DbContextOptions<MovieContext> options) : base(options)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Configurations (Models)
			builder.ApplyConfiguration(new GenreConfiguration());
			builder.ApplyConfiguration(new MovieConfiguration());
			builder.ApplyConfiguration(new PersonConfiguration());

			// Configurations (Model Associations)
			builder.ApplyConfiguration(new MovieGenreConfiguration());
			builder.ApplyConfiguration(new MoviePersonConfiguration());
		}
		#endregion
	}
}