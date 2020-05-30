using Memento.Movies.Client.Services.Genres;
using Memento.Movies.Client.Services.Movies;
using Memento.Movies.Client.Services.Persons;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Client.Shared.Routes;
using Memento.Movies.Shared.Models;
using Memento.Movies.Shared.Models.Contracts.Genres;
using Memento.Movies.Shared.Models.Contracts.Movies;
using Memento.Movies.Shared.Models.Contracts.Persons;
using Memento.Shared.Models.Repositories;
using Memento.Movies.Shared.Models.Repositories.Genres;
using Memento.Movies.Shared.Models.Repositories.Persons;
using Memento.Movies.Shared.Resources;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Memento.Movies.Client.Pages.Movies
{
	/// <summary>
	/// Implements the 'MovieForm' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	[Route(Routes.MovieRoutes.Create)]
	[Route(Routes.MovieRoutes.Update)]
	public sealed partial class MovieForm : MementoComponent<MovieForm>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The movie identifier.
		/// </summary>
		[Parameter]
		public long? MovieId { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The genre service.
		/// </summary>
		[Inject]
		public IGenreService GenreService { get; set; }
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
		/// The picture input image.
		/// </summary>
		public InputImage PictureInputImage { get; set; }

		/// <summary>
		/// The last genre to be selected through typeahead.
		/// </summary>
		private GenreListContract GenreModelTypeahead { get; set; }

		/// <summary>
		/// The genre input typeahead.
		/// </summary>
		public InputTypeahead<GenreListContract> GenreInputTypeahead { get; set; }

		/// <summary>
		/// The last person to be selected through typeahead.
		/// </summary>
		private PersonListContract PersonModelTypeahead { get; set; }

		/// <summary>
		/// The person input typeahead.
		/// </summary>
		public InputTypeahead<PersonListContract> PersonInputTypeahead { get; set; }

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
		/// The movie.
		/// </summary>
		private MovieDetailContract Movie { get; set; }

		/// <summary>
		/// The movie changes.
		/// </summary>
		private MovieFormContract MovieChanges { get; set; }

		/// <summary>
		/// The movie genres selected through typeahead.
		/// </summary>
		private List<GenreListContract> Genres { get; set; }

		/// <summary>
		/// The movie persons selected through typeahead.
		/// </summary>
		private Dictionary<MoviePersonRole, List<PersonListContract>> PersonsByRole { get; set; }

		/// <summary>
		/// The last movie person role to be selected.
		/// </summary>
		private MoviePersonRole? PersonRole { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			if (this.MovieId.HasValue)
			{
				// Get the movie
				var response = await this.MovieService.GetAsync(this.MovieId.Value);
				if (response.Success)
				{
					// Create the contracts
					this.Movie = response.Data;
					this.MovieChanges = this.Mapper.Map<MovieFormContract>(response.Data);

					// Show a toast message
					this.Toaster.Success(response.Message);
				}
				else
				{
					// Navigate to the list
					this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.Root));

					// Show a toast message
					this.Toaster.Error(response.Message);
				}
			}
			else
			{
				// Create the contracts
				this.Movie = new MovieDetailContract
				{
					ReleaseDate = DateTime.Today,
					Genres = new List<MovieGenreDetailContract>(),
					Persons = new List<MoviePersonDetailContract>()
				};
				this.MovieChanges = new MovieFormContract
				{
					ReleaseDate = DateTime.Today,
					Genres = new List<MovieGenreFormContract>(),
					Persons = new List<MoviePersonFormContract>()
				};
			}

			// Initialize the genres
			this.Genres = new List<GenreListContract>();
			foreach (var genre in this.Movie.Genres)
			{
				this.Genres.Add(this.Mapper.Map<GenreListContract>(genre));
			}

			// Initialize the persons
			this.PersonsByRole = new Dictionary<MoviePersonRole, List<PersonListContract>>();
			foreach (MoviePersonRole role in Enum.GetValues(typeof(MoviePersonRole)))
			{
				var persons = new List<PersonListContract>();

				foreach (var person in this.Movie.Persons.Where(p => p.Role == role))
				{
					persons.Add(this.Mapper.Map<PersonListContract>(person));
				}

				this.PersonsByRole.Add(role, persons);
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
			this.Toaster.Error(this.Localizer.GetString(SharedResources.FORM_INVALID_FIELDS));
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
			// Reset the genres
			this.MovieChanges.Genres.Clear();
			// Update the genres
			foreach (var genre in this.Genres)
			{
				this.MovieChanges.Genres.Add(this.Mapper.Map<MovieGenreFormContract>(genre));
			}

			// Reset the persons
			this.MovieChanges.Persons.Clear();
			// Update the persons
			foreach (var personsByRole in this.PersonsByRole)
			{
				foreach (var personByRole in personsByRole.Value)
				{
					var person = this.Mapper.Map<MoviePersonFormContract>(personByRole);
					person.Role = personsByRole.Key;

					this.MovieChanges.Persons.Add(person);
				}
			}

			if (this.MovieId.HasValue)
			{
				// Update the movie
				var response = await this.MovieService.UpdateAsync(this.MovieId.Value, this.MovieChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, this.MovieId.Value));

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
				// Create the movie
				var response = await this.MovieService.CreateAsync(this.MovieChanges);
				if (response.Success)
				{
					// Hide the modal
					await this.SaveChangesModal.HideAsync();

					// Navigate to the detail
					this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, response.Data.Id));

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

			if (this.MovieId.HasValue)
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.DetailIndexed, this.MovieId.Value));
			}
			else
			{
				// Navigate to the detail
				this.NavigationManager.NavigateTo(string.Format(Routes.MovieRoutes.Root));
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
		/// Gets the genres from the backend matching the given name.
		/// </summary>
		/// 
		/// <param name="name">The name.</param>
		public async Task<IEnumerable<GenreListContract>> GetGenresAsync(string name)
		{
			// Build the filter
			var genreFilter = new GenreFilter
			{
				Name = name,
				OrderBy = GenreFilterOrderBy.Name,
				OrderDirection = GenreFilterOrderDirection.Ascending
			};

			// Invoke the API
			var genres = await this.GenreService.GetAllAsync(genreFilter);

			// Filter the items
			return genres.Data.Items.Where(item => !this.Genres.Any(genre => genre.Id == item.Id));
		}

		/// <summary>
		/// Invoked when a genre is selected from the typeahead.
		/// </summary>
		/// 
		/// <param name="genre">The genre</param>
		public void OnGenreSelected(GenreListContract genre)
		{
			// Clear the bound genre
			this.GenreModelTypeahead = null;
			// Store the genre
			this.Genres.Add(genre);
			// Reset the typeahead
			this.GenreInputTypeahead.Reset();
		}

		/// <summary>
		/// Gets the persons from the backend matching the given name.
		/// </summary>
		/// 
		/// <param name="name">The name.</param>
		public async Task<IEnumerable<PersonListContract>> GetPersonsAsync(string name)
		{
			// Build the filter
			var personFilter = new PersonFilter
			{
				Name = name,
				OrderBy = PersonFilterOrderBy.Name,
				OrderDirection = PersonFilterOrderDirection.Ascending
			};

			// Invoke the API
			var persons = await this.PersonService.GetAllAsync(personFilter);

			// Filter the persons
			var filteredPersons = this.PersonsByRole.First(person => person.Key == this.PersonRole).Value;

			// Filter the items
			return persons.Data.Items.Where(item => !filteredPersons.Any(person => person.Id == item.Id));
		}

		/// <summary>
		/// Invoked when a person is selected from the typeahead.
		/// </summary>
		/// 
		/// <param name="person">The person</param>
		public void OnPersonSelected(PersonListContract person)
		{
			// Filter the persons
			var filteredPersons = this.PersonsByRole.First(person => person.Key == this.PersonRole).Value;

			// Clear the bound person
			this.PersonModelTypeahead = null;
			// Store the person
			filteredPersons.Add(person);
			// Reset the typeahead
			this.PersonInputTypeahead.Reset();
		}
		#endregion
	}
}