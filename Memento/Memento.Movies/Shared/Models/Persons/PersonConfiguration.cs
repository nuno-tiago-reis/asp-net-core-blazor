using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models.Persons
{
	/// <summary>
	/// Implements the 'Person' configurations.
	/// </summary>
	/// 
	/// <seealso cref="Person" />
	public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
	{
		#region [Constants]
		/// <summary>
		/// The maximum length for the name column.
		/// </summary>
		public const int NAME_MAXIMUM_LENGTH = 250;

		/// <summary>
		/// The maximum length for the biography column.
		/// </summary>
		public const int BIOGRAPHY_MAXIMUM_LENGTH = 500;

		/// <summary>
		/// The maximum length for the picture url column.
		/// </summary>
		public const int PICTURE_URL_MAXIMUM_LENGTH = 250;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			// Keys
			builder.HasKey(person => person.Id);

			// Indices
			builder.HasIndex(person => person.Name);
			builder.HasIndex(person => person.NormalizedName);
			builder.HasIndex(person => person.BirthDate);

			// Indices (Compound)
			builder.HasIndex(person => new { person.NormalizedName, person.BirthDate }).IsUnique();

			// Properties (Person)
			builder.Property(person => person.Name).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);
			builder.Property(person => person.NormalizedName).IsRequired().HasMaxLength(NAME_MAXIMUM_LENGTH);
			builder.Property(person => person.Biography).IsRequired().HasMaxLength(BIOGRAPHY_MAXIMUM_LENGTH);
			builder.Property(person => person.PictureUrl).IsRequired().HasMaxLength(PICTURE_URL_MAXIMUM_LENGTH);
			builder.Property(person => person.BirthDate).IsRequired();

			// Properties (Model)
			builder.Property(person => person.CreatedBy).IsRequired();
			builder.Property(person => person.CreatedAt).IsRequired();
			builder.Property(person => person.UpdatedBy);
			builder.Property(person => person.UpdatedAt);
		}
		#endregion
	}
}