using Memento.Movies.Shared.Models.Movies;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieCard' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieCard : BaseComponent<MovieCard>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie.
		/// </summary>
		[Parameter]
		public Movie Movie { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }
		#endregion
	}
}