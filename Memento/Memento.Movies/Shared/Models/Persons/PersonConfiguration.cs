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
			builder.Property(person => person.Name).IsRequired().HasMaxLength(250);
			builder.Property(person => person.NormalizedName).IsRequired().HasMaxLength(250);
			builder.Property(person => person.Biography).IsRequired().HasMaxLength(500);
			builder.Property(person => person.PictureUrl).IsRequired().HasMaxLength(250);
			builder.Property(person => person.BirthDate).IsRequired();

			// Properties (Model)
			builder.Property(person => person.CreatedBy).IsRequired();
			builder.Property(person => person.CreatedAt).IsRequired();
			builder.Property(person => person.UpdatedBy);
			builder.Property(person => person.UpdatedAt);
		}
	}
}