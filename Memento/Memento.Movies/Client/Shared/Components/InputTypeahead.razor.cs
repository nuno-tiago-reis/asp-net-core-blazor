using Memento.Movies.Shared.Resources;
using Memento.Shared.Extensions;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Internal;
using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

using Timer = System.Timers.Timer;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a label for a specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputTypeahead<T> : InputBase<T>
	{
		#region [Constants]
		/// <summary>
		/// The default search minimum length.
		/// </summary>
		private const int DEFAULT_SEARCH_MINIMUM_LENGTH = 3;

		/// <summary>
		/// The default search minimum interval (milliseconds).
		/// </summary>
		private const int DEFAULT_SEARCH_MINIMUM_INTERVAL = 300;
		#endregion

		#region [Properties] Parameters
		/// <summary>
		/// The minimum length required to invoke the search method.
		/// </summary>
		[Parameter]
		public int SearchMinimumLength { get; set; }

		/// <summary>
		/// The minimum time required to invoke the search method again (in milliseconds).
		/// </summary>
		[Parameter]
		public int SearchMinimumInterval { get; set; }

		/// <summary>
		/// The search method.
		/// </summary>
		[Parameter]
		public Func<string, Task<IEnumerable<T>>> SearchMethod { get; set; }

		/// <summary>
		/// The suggestion content.
		/// </summary>
		[Parameter] 
		public RenderFragment<T> SuggestionContent { get; set; }

		/// <summary>
		/// The searching content.
		/// </summary>
		[Parameter]
		public RenderFragment SearchingContent { get; set; }

		/// <summary>
		/// The not found content.
		/// </summary>
		[Parameter] 
		public RenderFragment NotFoundContent { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The localizer service.
		/// </summary>
		[Inject]
		private ILocalizerService Localizer { get; set; }

		/// <summary>
		/// The toaster service.
		/// </summary>
		[Inject]
		private IToaster Toaster { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The state.
		/// </summary>
		private InputTypeaheadState State { get; set; }

		/// <summary>
		/// Whether the mouse is hovering the input.
		/// </summary>
		private bool Hovering { get; set; }

		/// <summary>
		/// The search text.
		/// </summary>
		private string SearchText { get; set; }

		/// <summary>
		/// The search timer.
		/// </summary>
		private Timer SearchTimer { get; set; }

		/// <summary>
		/// The suggestions loaded from the search text.
		/// </summary>
		private IEnumerable<T> SearchSuggestions { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Initialize the search restrictions
			this.SearchMinimumLength = DEFAULT_SEARCH_MINIMUM_LENGTH;
			this.SearchMinimumInterval = DEFAULT_SEARCH_MINIMUM_INTERVAL;
		}

		/// <inheritdoc />
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// Validations
			if (this.EditContext == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a cascading parameter of type {nameof(EditContext)}. " +
					$"For example, you can use {this.GetType()} inside an {nameof(EditForm)}."
				);
			}

			if (this.SuggestionContent == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.SuggestionContent)} parameter."
				);
			}

			if (this.SearchMethod == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.SearchMethod)} parameter."
				);
			}

			// Initialize the search state
			this.State = InputTypeaheadState.Idle;
			this.SearchSuggestions = new List<T>();
			this.SearchTimer = new Timer
			{
				AutoReset = false,
				Interval = this.SearchMinimumInterval
			};

			// Initialize the search delegate
			this.SearchTimer.Elapsed += this.OnSearchAsync;
		}

		/// <inheritdoc />
		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);

			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override bool ShouldRender()
		{
			return base.ShouldRender();

			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
		{
			result = default;
			validationErrorMessage = string.Empty;

			return true;
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Invoked when the search is triggered.
		/// </summary>
		/// 
		/// <param name="source">The source.</param>
		/// <param name="arguments">The arguments.</param>
		private async void OnSearchAsync(Object source, ElapsedEventArgs arguments)
		{
			// Change the state to searching
			this.State = InputTypeaheadState.Searching;
			this.StateHasChanged();

			try
			{
				// Invoke the search method
				this.SearchSuggestions = await this.SearchMethod.Invoke(this.SearchText);
				if (this.SearchSuggestions.Count() > 0)
				{
					// Change the state to searched with results
					this.State = InputTypeaheadState.SearchedWithResults;
					this.StateHasChanged();
				}
				else
				{
					// Change the state to searched with no results
					this.State = InputTypeaheadState.SearchedWithoutResults;
					this.StateHasChanged();
				}
			}
			catch
			{
				// Change the state to searched with no results
				this.State = InputTypeaheadState.SearchedWithoutResults;
				this.StateHasChanged();

				// Show a toast message
				this.Toaster.Error(this.Localizer.GetString(SharedResources.ERROR_UNEXPECTED));
			}
		}

		/// <summary>
		/// Invoked when the input gains focus.
		/// </summary>
		private void OnInputFocusIn()
		{
			if (!string.IsNullOrWhiteSpace(this.SearchText))
			{
				// Attempt to trigger a search
				this.OnInputChange(new ChangeEventArgs { Value = this.SearchText });
			}
		}

		/// <summary>
		/// Invoked when the input loses focus.
		/// </summary>
		private void OnInputFocusOut()
		{
			if (this.Hovering == false)
			{
				// Change the state to idle
				this.State = InputTypeaheadState.Idle;
				this.StateHasChanged();
			}
		}

		/// <summary>
		/// Invoked when the inputs value changes.
		/// </summary>
		/// 
		/// <param name="arguments">The arguments.</param>
		private void OnInputChange(ChangeEventArgs arguments)
		{
			// Update the search text
			this.SearchText = arguments.Value.ToString();

			// Update the search timer
			if (string.IsNullOrWhiteSpace(this.SearchText))
			{
				this.SearchSuggestions = new List<T>();
			}
			else if (this.SearchText.Length < this.SearchMinimumLength)
			{
				this.SearchTimer.Stop();
			}
			else if (this.SearchText.Length >= this.SearchMinimumLength)
			{
				this.SearchTimer.Reset();
			}

			// Update the search state
			this.State = InputTypeaheadState.Idle;
			this.StateHasChanged();
		}

		/// <summary>
		/// Invoked when the mouse enters a suggestion.
		/// </summary>
		private void OnSuggestionMouseEnter()
		{
			this.Hovering = true;
		}

		/// <summary>
		/// Invoked when the mouse leaves a suggestion.
		/// </summary>
		private void OnSuggestionMouseLeave()
		{
			this.Hovering = false;
		}

		/// <summary>
		/// Invoked when a suggestion is cliced.
		/// </summary>
		/// 
		/// <param name="suggestion">The suggestion.</param>
		private async Task OnSuggestionClickAsync(T suggestion)
		{
			// Change the value
			this.Value = suggestion;
			await this.ValueChanged.InvokeAsync(suggestion);

			// Change the state to idle
			this.State = InputTypeaheadState.Idle;
			this.StateHasChanged();
		}
		#endregion

		#region [Methods] Typeahead
		/// <summary>
		/// Resets the typeahead state.
		/// </summary>
		public void Reset()
		{
			// Change the text
			this.SearchText = string.Empty;

			// Change the value
			this.Value = default;

			// Change the state to idle
			this.State = InputTypeaheadState.Idle;
			this.StateHasChanged();
		}
		#endregion
	}

	/// <summary>
	/// Defines the states the typeahead can have.
	/// </summary>
	public enum InputTypeaheadState
	{
		/// <summary>
		/// The typeahead is idle.
		/// </summary>
		Idle,
		/// <summary>
		/// The typeahead is searching.
		/// </summary>
		Searching,
		/// <summary>
		/// The typeahead search found results.
		/// </summary>
		SearchedWithResults,
		/// <summary>
		/// The typeahead search found no results.
		/// </summary>
		SearchedWithoutResults
	}
}