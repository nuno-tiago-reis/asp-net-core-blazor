using Memento.Movies.Shared.Database.Models.Movies;
using Memento.Movies.Shared.Database.Models.Persons;
using Memento.Shared.Models;

namespace Memento.Movies.Shared.Database.Models
{
	/// <summary>
	/// Implements the 'MoviePerson' model.
	/// </summary>
	public sealed class MoviePerson : Model
	{
		#region [Properties]
		/// <summary>
		/// The 'Movie' identifier.
		/// </summary>
		public long MovieId { get; set; }

		/// <summary>
		/// The 'Movie' navigation property.
		/// </summary>
		public Movie Movie { get; set; }

		/// <summary>
		/// The 'Person' identifier.
		/// </summary>
		public long PersonId { get; set; }

		/// <summary>
		/// The 'Person' navigation property.
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		/// The persons role.
		/// </summary>
		public PersonRole PersonRole { get; set; }
		#endregion
	}

	/// <summary>
	/// Defines the roles a person can have in a movie.
	/// </summary>
	public enum PersonRole
	{
		/// <summary>
		/// The person is an actor.
		/// </summary>
		Actor = 0,
		/// <summary>
		/// The person is a director
		/// </summary>
		Director = 1,
		/// <summary>
		/// The person is a writer.
		/// </summary>
		Writer = 2
	}
}