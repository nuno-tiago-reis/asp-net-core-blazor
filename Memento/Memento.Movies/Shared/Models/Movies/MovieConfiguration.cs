using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models.Movies
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
			builder.HasIndex(movie => movie.Name);
			builder.HasIndex(movie => movie.NormalizedName);
			builder.HasIndex(movie => movie.ReleaseDate);

			// Indices (Compound)
			builder.HasIndex(movie => new { movie.NormalizedName, movie.ReleaseDate }).IsUnique();

			// Properties
			builder.Property(movie => movie.Name).IsRequired().HasMaxLength(250);
			builder.Property(movie => movie.NormalizedName).IsRequired().HasMaxLength(250);
			builder.Property(movie => movie.Summary).IsRequired().HasMaxLength(500);
			builder.Property(movie => movie.PictureUrl).IsRequired().HasMaxLength(250);
			builder.Property(movie => movie.TrailerUrl).IsRequired().HasMaxLength(250);
			builder.Property(movie => movie.ReleaseDate).IsRequired();
			builder.Property(movie => movie.InTheaters).IsRequired();

			// Properties (Model)
			builder.Property(movie => movie.CreatedBy).IsRequired();
			builder.Property(movie => movie.CreatedAt).IsRequired();
			builder.Property(movie => movie.UpdatedBy);
			builder.Property(movie => movie.UpdatedAt);
		}
	}
}