using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models.Repositories.Movies
{
	/// <summary>
	/// Implements the 'Movie' configurations.
	/// </summary>
	/// 
	/// <seealso cref="Movie" />
	public sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		#region [Constants]
		/// <summary>
		/// The maximum length for the name column.
		/// </summary>
		public const int NAME_MAXIMUM_LENGTH = 250;

		/// <summary>
		/// The maximum length for the summary column.
		/// </summary>
		public const int SUMMARY_MAXIMUM_LENGTH = 500;

		/// <summary>
		/// The maximum length for the picture url column.
		/// </summary>
		public const int PICTURE_URL_MAXIMUM_LENGTH = 250;

		/// <summary>
		/// The maximum length for the trailer url column.
		/// </summary>
		public const int TRAILER_URL_MAXIMUM_LENGTH = 250;
		#endregion

		#region [Methods]
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
			builder.Property(movie => movie.Name).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);
			builder.Property(movie => movie.NormalizedName).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);
			builder.Property(movie => movie.Summary).IsRequired().HasMaxLength(SUMMARY_MAXIMUM_LENGTH);
			builder.Property(movie => movie.PictureUrl).IsRequired().HasMaxLength(PICTURE_URL_MAXIMUM_LENGTH);
			builder.Property(movie => movie.TrailerUrl).IsRequired().HasMaxLength(TRAILER_URL_MAXIMUM_LENGTH);
			builder.Property(movie => movie.ReleaseDate).IsRequired();
			builder.Property(movie => movie.InTheaters).IsRequired();

			// Properties (Model)
			builder.Property(movie => movie.CreatedBy).IsRequired();
			builder.Property(movie => movie.CreatedAt).IsRequired();
			builder.Property(movie => movie.UpdatedBy);
			builder.Property(movie => movie.UpdatedAt);
		}
		#endregion
	}
}