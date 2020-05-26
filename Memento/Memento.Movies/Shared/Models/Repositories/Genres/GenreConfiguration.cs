using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models.Repositories.Genres
{
	/// <summary>
	/// Implements the 'Genre' configurations.
	/// </summary>
	/// 
	/// <seealso cref="Genre" />
	public sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		#region [Constants]
		/// <summary>
		/// The maximum length for the name column.
		/// </summary>
		public const int NAME_MAXIMUM_LENGTH = 50;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			// Keys
			builder.HasKey(genre => genre.Id);

			// Indices
			builder.HasIndex(genre => genre.Name).IsUnique();
			builder.HasIndex(genre => genre.NormalizedName).IsUnique();

			// Properties (Genre)
			builder.Property(genre => genre.Name).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);
			builder.Property(genre => genre.NormalizedName).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);

			// Properties (Model)
			builder.Property(genre => genre.CreatedBy).IsRequired();
			builder.Property(genre => genre.CreatedAt).IsRequired();
			builder.Property(genre => genre.UpdatedBy);
			builder.Property(genre => genre.UpdatedAt);
		}
		#endregion
	}
}