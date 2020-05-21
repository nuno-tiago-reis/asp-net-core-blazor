using Memento.Movies.Shared.Database.Models.Movies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieList : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movies.
		/// </summary>
		[Parameter]
		public List<Movie> Movies { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie repository.
		/// </summary>
		[Inject]
		public IMovieRepository Repository { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(1000);

			this.Movies = (await this.Repository.GetAllAsync()).ToList();
		}
		#endregion
	}
}