using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models.Movies
{
	/// <summary>
	/// Implements the 'MovieGenre' configurations.
	/// </summary>
	/// 
	/// <seealso cref="IEntityTypeConfiguration{MovieGenre}" />
	public sealed class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
	{
		#region [Methods]
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<MovieGenre> builder)
		{
			// Keys
			builder.HasKey(movieGenre => movieGenre.Id);

			// Indices
			builder.HasIndex(movieGenre => movieGenre.GenreId);
			builder.HasIndex(movieGenre => movieGenre.MovieId);

			// Properties (MovieGenre)
			builder.Property(movieGenre => movieGenre.GenreId).IsRequired();
			builder.Property(movieGenre => movieGenre.MovieId).IsRequired();

			// Properties (Model)
			builder.Property(movieGenre => movieGenre.CreatedBy).IsRequired();
			builder.Property(movieGenre => movieGenre.CreatedAt).IsRequired();
			builder.Property(movieGenre => movieGenre.UpdatedBy);
			builder.Property(movieGenre => movieGenre.UpdatedAt);

			// Relations
			builder
				.HasOne(movieGenre => movieGenre.Genre)
				.WithMany(genre => genre.Movies)
				.HasForeignKey(movieGenre => movieGenre.GenreId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(movieGenre => movieGenre.Movie)
				.WithMany(movie => movie.Genres)
				.HasForeignKey(movieGenre => movieGenre.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
		}
		#endregion
	}
}