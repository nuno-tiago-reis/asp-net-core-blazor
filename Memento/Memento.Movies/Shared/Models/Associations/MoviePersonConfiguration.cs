using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memento.Movies.Shared.Models
{
	/// <summary>
	/// Implements the 'MoviePerson' configurations.
	/// </summary>
	/// 
	/// <seealso cref="IEntityTypeConfiguration{MoviePerson}" />
	public sealed class MoviePersonConfiguration : IEntityTypeConfiguration<MoviePerson>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<MoviePerson> builder)
		{
			// Keys
			builder.HasKey(moviePerson => moviePerson.Id);

			// Indices
			builder.HasIndex(moviePerson => moviePerson.MovieId);
			builder.HasIndex(moviePerson => moviePerson.PersonId);

			// Properties (MoviePerson)
			builder.Property(moviePerson => moviePerson.MovieId).IsRequired();
			builder.Property(moviePerson => moviePerson.PersonId).IsRequired();
			builder.Property(moviePerson => moviePerson.PersonRole).IsRequired();

			// Properties (Model)
			builder.Property(moviePerson => moviePerson.CreatedBy).IsRequired();
			builder.Property(moviePerson => moviePerson.CreatedAt).IsRequired();
			builder.Property(moviePerson => moviePerson.UpdatedBy);
			builder.Property(moviePerson => moviePerson.UpdatedAt);

			// Relations
			builder
				.HasOne(moviePerson => moviePerson.Movie)
				.WithMany(movie => movie.Persons)
				.HasForeignKey(movieGenre => movieGenre.MovieId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(moviePerson => moviePerson.Person)
				.WithMany(person => person.Movies)
				.HasForeignKey(moviePerson => moviePerson.PersonId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}