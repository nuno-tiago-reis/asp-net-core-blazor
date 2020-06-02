using Blazorade.Bootstrap.Components;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'PageListFilter' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PageListFilter<T> : MementoComponent<PageListFilter<T>>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The filter.
		/// </summary>
		[Parameter]
		public T Filter { get; set; }

		/// <summary>
		/// Gets or sets filter child content to be rendered inside the collapse.
		/// </summary>
		[Parameter]
		public RenderFragment FilterContent { get; set; }

		/// <summary>
		/// The search button callback.
		/// </summary>
		[Parameter]
		public EventCallback<T> OnSearch { get; set; }

		/// <summary>
		/// The reset button callback.
		/// </summary>
		[Parameter]
		public EventCallback<T> OnReset { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The collapse.
		/// </summary>
		private Collapse Collapse { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// Whether the collapse is collapsed.
		/// </summary>
		private bool IsCollapsed { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Validations
			if (this.Filter == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.Filter)} parameter."
				);
			}

			if (this.OnSearch.HasDelegate == false)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.OnSearch)} parameter."
				);
			}

			if (this.OnReset.HasDelegate == false)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.OnReset)} parameter."
				);
			}

			// Initializations
			this.IsCollapsed = true;
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// Nothing to do here.
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);

			// Nothing to do here.
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override bool ShouldRender()
		{
			return base.ShouldRender();

			// Nothing to do here.
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Callback that is invoked when the user clicks the filters toggle button.
		/// </summary>
		private async Task OnFilterToggleAsync()
		{
			if (this.IsCollapsed)
			{
				await this.Collapse.ShowAsync();
			}
			else
			{
				await this.Collapse.HideAsync();
			}

			this.IsCollapsed = !this.IsCollapsed;
			this.StateHasChanged();
		}

		/// <summary>
		/// Callback that is invoked when the user clicks the filters search button.
		/// </summary>
		private async Task OnSubmitAsync()
		{
			await this.OnSearch.InvokeAsync(this.Filter);
		}

		/// <summary>
		/// Callback that is invoked when the user clicks the filters reset button.
		/// </summary>
		private async Task OnResetAsync()
		{
			await this.OnReset.InvokeAsync(this.Filter);
		}
		#endregion
	}
}