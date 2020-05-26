using AutoMapper;
using Memento.Movies.Shared.Contracts.Genres;
using Memento.Movies.Shared.Models.Genres;
using Memento.Shared.Configuration;

namespace Memento.Movies.Shared.Configuration
{
	/// <summary>
	/// Implements the 'MovieMapper' profile.
	/// </summary>
	/// 
	/// <seealso cref="Profile" />
	public sealed class MovieMapperProfile : MementoMapperProfile
	{
		#region [Constructor]
		/// <summary>
		/// Initializes a new instance of the <see cref="MovieMapperProfile"/> class.
		/// </summary>
		public MovieMapperProfile() : base()
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void CreateMappings()
		{
			base.CreateMappings();

			#region [Contracts: Genres]
			// Genres: Model => Contract
			this.CreateMap<Genre, GenreDetailContract>();

			// Genres: Model => Contract
			this.CreateMap<Genre, GenreListContract>();

			// Genres: Contract => Model
			this.CreateMap<GenreFormContract, Genre>();

			// Genres: Model => Contract
			this.CreateMap<Genre, GenreFormContract>();

			// Genres: Contract => Contract
			this.CreateMap<GenreFormContract, GenreDetailContract>();

			// Genres: Contract => Contract
			this.CreateMap<GenreDetailContract, GenreFormContract>();
			#endregion
		}
		#endregion
	}
}