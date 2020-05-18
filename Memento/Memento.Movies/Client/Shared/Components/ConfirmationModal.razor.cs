using Blazorade.Bootstrap.Components;
using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'ConfirmationModal' component.
	/// </summary>
	/// 
	/// <seealso cref="MementoComponent{ConfirmationModal}"/>
	public sealed partial class ConfirmationModal : MementoComponent<ConfirmationModal>
	{
		#region [Properties] Parameters
		/// <summary>
		/// The header background color.
		/// </summary>
		[Parameter]
		public NamedColor HeaderBackgroundColor { get; set; }

		/// <summary>
		/// The header text color.
		/// </summary>
		[Parameter]
		public NamedColor HeaderTextColor { get; set; }

		/// <summary>
		/// The header content
		/// </summary>
		[Parameter]
		public RenderFragment HeaderContent { get; set; }

		/// <summary>
		/// The body content.
		/// </summary>
		[Parameter]
		public RenderFragment BodyContent { get; set; }

		/// <summary>
		/// The confirm button icon.
		/// </summary>
		[Parameter]
		public string ConfirmButtonIcon { get; set; }

		/// <summary>
		/// The confirm button color.
		/// </summary>
		[Parameter]
		public NamedColor ConfirmButtonColor { get; set; }

		/// <summary>
		/// The confirm button content.
		/// </summary>
		[Parameter]
		public RenderFragment ConfirmButtonContent { get; set; }

		/// <summary>
		/// The confirm button callback.
		/// </summary>
		[Parameter]
		public EventCallback<Button> ConfirmButtonCallback { get; set; }

		/// <summary>
		/// The cancel button icon.
		/// </summary>
		[Parameter]
		public string CancelButtonIcon { get; set; }

		/// <summary>
		/// The cancel button color.
		/// </summary>
		[Parameter]
		public NamedColor CancelButtonColor { get; set; }

		/// <summary>
		/// The cancel button content.
		/// </summary>
		[Parameter]
		public RenderFragment CancelButtonContent { get; set; }

		/// <summary>
		/// The cancel button callback.
		/// </summary>
		[Parameter]
		public EventCallback<Button> CancelButtonCallback { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The modal.
		/// </summary>
		private Modal Modal { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfirmationModal"/> class.
		/// </summary>
		public ConfirmationModal()
		{
			this.HeaderBackgroundColor = NamedColor.Primary;
			this.HeaderTextColor = NamedColor.White;
			this.ConfirmButtonColor = NamedColor.Success;
			this.CancelButtonColor = NamedColor.Secondary;
		}
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Nothing to do here.
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// Validations
			if (this.ConfirmButtonCallback.HasDelegate == false)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.ConfirmButtonCallback)} parameter."
				);
			}

			if (this.CancelButtonCallback.HasDelegate == false)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.CancelButtonCallback)} parameter."
				);
			}
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

		#region [Methods] Modal
		/// <summary>
		/// Shows the modal.
		/// </summary>
		public async Task ShowAsync()
		{
			await this.Modal.ShowAsync();
		}

		/// <summary>
		/// Hides the modal.
		/// </summary>
		public async Task HideAsync()
		{
			await this.Modal.HideAsync();
		}

		/// <summary>
		/// Invoked when the confirm button is clicked.
		/// </summary>
		///
		/// <param name="button">The button.</param> 
		private async Task OnConfirmAsync(Button button)
		{
			// Wait for the callback to complete
			await this.ConfirmButtonCallback.InvokeAsync(button);

			// Hide the modal
			await this.HideAsync();
		}

		/// <summary>
		/// Invoked when the cancel button is clicked.
		/// </summary>
		///
		/// <param name="button">The button.</param> 
		private async Task OnCancelAsync(Button button)
		{
			// Wait for the callback to complete
			await this.CancelButtonCallback.InvokeAsync(button);

			// Hide the modal
			await this.HideAsync();
		}
		#endregion
	}
}