using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Database.Models.Genres
{
	/// <summary>
	/// Implements the 'Genre' configurations.
	/// </summary>
	/// 
	/// <seealso cref="Genre" />
	public sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			// Keys
			builder.HasKey(genre => genre.Id);

			// Indices
			builder.HasIndex(genre => genre.Name).IsUnique();
			builder.HasIndex(genre => genre.NormalizedName).IsUnique();

			// Properties (Genre)
			builder.Property(genre => genre.Name).IsRequired().HasMaxLength(50);
			builder.Property(genre => genre.NormalizedName).IsRequired().HasMaxLength(50);

			// Properties (Model)
			builder.Property(genre => genre.CreatedBy).IsRequired();
			builder.Property(genre => genre.CreatedAt).IsRequired();
			builder.Property(genre => genre.UpdatedBy);
			builder.Property(genre => genre.UpdatedAt);
		}
	}
}