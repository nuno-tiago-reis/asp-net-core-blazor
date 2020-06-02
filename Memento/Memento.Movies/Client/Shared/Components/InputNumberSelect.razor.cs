using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays an integer select input for specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputNumberSelect : InputBase<int>
	{
		#region [Properties] Parameters
		/// <summary>
		/// Gets or sets the child content to be rendering inside the select element.
		/// </summary>
		[Parameter]
		public RenderFragment ChildContent { get; set; }
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
			if (this.EditContext == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a cascading parameter of type {nameof(EditContext)}. " +
					$"For example, you can use {this.GetType()} inside an {nameof(EditForm)}."
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

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override bool TryParseValueFromString(string value, out int result, out string validationErrorMessage)
		{
			if (int.TryParse(value, out var @int))
			{
				result = @int;
				validationErrorMessage = null;
				return true;
			}
			else
			{
				result = default;
				validationErrorMessage = "The chosen value is not a valid number.";
				return false;
			}
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Invoked when the inputs value changes.
		/// </summary>
		/// 
		/// <param name="arguments">The arguments.</param>
		private void OnInputChange(ChangeEventArgs arguments)
		{
			this.CurrentValueAsString = arguments.Value.ToString();
			this.StateHasChanged();
		}
		#endregion
	}
}