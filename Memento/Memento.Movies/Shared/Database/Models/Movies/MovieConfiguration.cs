using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Database.Models.Movies
{
	/// <summary>
	/// Implements the 'Movie' configurations.
	/// </summary>
	/// 
	/// <seealso cref="Movie" />
	public sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			// Keys
			builder.HasKey(movie => movie.Id);

			// Indices
			builder.HasIndex(movie => movie.Title).IsUnique();
			builder.HasIndex(movie => movie.Genre);
			builder.HasIndex(movie => movie.ReleaseDate);

			// Properties
			builder.Property(movie => movie.Title).IsRequired().HasMaxLength(250);
			builder.Property(movie => movie.Genre).IsRequired().HasMaxLength(50);
			builder.Property(movie => movie.ReleaseDate).IsRequired();
			builder.Property(movie => movie.CreatedBy).IsRequired();
			builder.Property(movie => movie.CreatedAt).IsRequired();
			builder.Property(movie => movie.UpdatedBy);
			builder.Property(movie => movie.UpdatedAt);
		}
	}
}