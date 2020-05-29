using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Movies.Shared.Models.Repositories.Movies;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Memento.Shared.Models.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Pages.Persons
{
	/// <summary>
	/// Implements the 'PersonForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.PersonRoutes.Create)]
	[Route(Routes.PersonRoutes.Update)]
	public sealed partial class PersonForm : MementoComponent<PersonForm>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The person identifier.
		/// </summary>
		[Parameter]
		public long? PersonId { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The movie service.
		/// </summary>
		[Inject]
		public IMovieService MovieService { get; set; }

		/// <summary>
		/// The person service.
		/// </summary>
		[Inject]
		public IPersonService PersonService { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The input image.
		/// </summary>
		public InputImage InputImage { get; set; }

		/// <summary>
		/// The last movie to be selected through typeahead.
		/// </summary>
		private MovieListContract MovieModelTypeahead { get; set; }

		/// <summary>
		/// The movie input typeahead.
		/// </summary>
		public InputTypeahead<MovieListContract> MovieInputTypeahead { get; set; }

		/// <summary>
		/// The save changes modal.
		/// </summary>
		public ConfirmationModal SaveChangesModal { get; set; }

		/// <summary>
		/// The discard changes modal.
		/// </summary>
		public ConfirmationModal DiscardChangesModal { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The person.
		/// </summary>
		private PersonDetailContract Person { get; set; }

		/// <summary>
		/// The person changes.
		/// </summary>
		private PersonFormContract PersonChanges { get; set; }

		/// <summary>
		/// The persons movies selected through typeahead.
		/// </summary>
		private Dictionary<MoviePersonRole, List<MovieListContract>> MoviesByRole { get; set; }

		/// <summary>
		/// The last movie person role to be selected.
		/// </summary>
		private MoviePersonRole? MovieRole { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			if (this.PersonId.HasValue)
			{
				// Get the person
				var response = await this.PersonService.GetAsync(this.PersonId.Value);
				if (response.Success)
				{
					// Create the contract
					this.Person = response.Data;
					this.PersonChanges = this.Mapper.Map<PersonFormContract>(response.Data);

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Navigate to the list
					this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.Root));

					// Show a toast message
					this.Toaster.Error(response.Message);
				}
			}
			else
			{
				// Create the contracts
				this.Person = new PersonDetailContract
				{
					BirthDate = DateTime.Today,
					Movies = new List<PersonMovieContract>()
				};
				this.PersonChanges = new PersonFormContract
				{
					BirthDate = DateTime.Today,
					Movies = new List<Tuple<long, MoviePersonRole>>()
				};
			}

			// Initialize the movies
			this.MoviesByRole = new Dictionary<MoviePersonRole, List<MovieListContract>>();
			foreach (MoviePersonRole role in Enum.GetValues(typeof(MoviePersonRole)))
			{
				var movies = new List<MovieListContract>();

				foreach (var movie in this.Person.Movies.Where(m => m.Role == role))
				{
					movies.Add(this.Mapper.Map<MovieListContract>(movie));
				}

				this.MoviesByRole.Add(role, movies);
			}
		}
		#endregion

		#region [Methods] Form
		/// <summary>
		/// Callback that is invoked when the form is submited with no errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public async Task OnValidSubmitAsync(EditContext _)
		{
			await this.SaveChangesModal.ShowAsync();
		}

		/// <summary>
		/// Callback that is invoked when the form is submited with errors.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>.</param>
		public void OnInvalidSubmit(EditContext _)
		{
			// Show a toast message
			this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_FORM_INVALID_FIELDS));
		}

		/// <summary>
		/// Callback that is invoked when the form is cancelled.
		/// </summary>
		public async Task OnCancelAsync()
		{
			await this.DiscardChangesModal.ShowAsync();
		}
		#endregion

		#region [Methods] Save Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnSaveChangesConfirmedAsync()
		{
			// Update the movies
			foreach (var movies in this.MoviesByRole)
			{
				foreach (var movie in movies.Value)
				{
					this.PersonChanges.Movies.Add(new Tuple<long, MoviePersonRole>(movie.Id, movies.Key));
				}
			}

			if (this.PersonId.HasValue)
			{
				// Update the person
				var response = await this.PersonService.UpdateAsync(this.PersonId.Value, this.PersonChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.DetailIndexed, this.PersonId.Value));

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Show a toast message
					this.Toaster.Error(string.Join("\r\n", response.Errors), response.Message);
				}
			}
			else
			{
				// Create the person
				var response = await this.PersonService.CreateAsync(this.PersonChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.DetailIndexed, response.Data.Id));

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Show a toast message
					this.Toaster.Error(string.Join("\r\n", response.Errors), response.Message);
				}
			}
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the cancel button in the save changes modal.
		/// </summary>
		public async Task OnSaveChangesCancelledAsync()
		{
			// Hide the modal
			await this.SaveChangesModal.HideAsync();
		}
		#endregion

		#region [Methods] Discard Changes Modal
		/// <summary>
		/// Callback that is invoked when the user clicks on the confirm button in the save changes modal.
		/// </summary>
		public async Task OnDiscardChangesConfirmedAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();

			if (this.PersonId.HasValue)
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.DetailIndexed, this.PersonId.Value));
			}
			else
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.PersonRoutes.Root));
			}
		}

		/// <summary>
		/// Callback that is invoked when the user clicks on the cancel button in the discard changes modal.
		/// </summary>
		public async Task OnDiscardChangesCancelledAsync()
		{
			// Hide the modal
			await this.DiscardChangesModal.HideAsync();
		}
		#endregion

		#region [Methods] Typeahead
		/// <summary>
		/// Gets the movies from the backend matching the given name.
		/// </summary>
		/// 
		/// <param name="name">The name.</param>
		public async Task<IEnumerable<MovieListContract>> GetMoviesAsync(string name)
		{
			// Build the filter
			var movieFilter = new MovieFilter
			{
				Name = name,
				OrderBy = MovieFilterOrderBy.Name,
				OrderDirection = FilterOrderDirection.Ascending
			};

			// Invoke the API
			var movies = await this.MovieService.GetAllAsync(movieFilter);

			// Filter the persons
			var filteredMovies = this.MoviesByRole.First(movie => movie.Key == this.MovieRole).Value;

			// Filter the items
			return movies.Data.Items.Where(item => !filteredMovies.Any(movie => movie.Id == item.Id));
		}

		/// <summary>
		/// Invoked when a movie is selected from the typeahead.
		/// </summary>
		/// 
		/// <param name="movie">The movie</param>
		public void OnMovieSelected(MovieListContract movie)
		{
			// Filter the movies
			var filteredMovies = this.MoviesByRole.First(movie => movie.Key == this.MovieRole).Value;

			// Clear the bound movie
			this.MovieModelTypeahead = null;
			// Store the movie
			filteredMovies.Add(movie);
			// Reset the typeahead
			this.MovieInputTypeahead.Reset();
		}
		#endregion
	}
}