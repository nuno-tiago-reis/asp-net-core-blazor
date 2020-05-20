using Memento.Movies.Shared.Database.Models.Movies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Movies
{
	/// <summary>
	/// Implements the 'MovieList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieList : ComponentBase
	{
		#region [Properties]
		/// <summary>
		/// The movies.
		/// </summary>
		[Parameter]
		public List<Movie> Movies { get; set; }

		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(3000);

			this.Movies = (await this.Repository.GetAllAsync()).ToList();
		}
		#endregion
	}
}