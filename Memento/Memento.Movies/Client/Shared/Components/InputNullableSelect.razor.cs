using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Globalization;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a nullable select input for specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputNullableSelect<T> : InputBase<T>
	{
		#region [Properties] Parameters
		/// <summary>
		/// Gets or sets the child content to be rendering inside the select element.
		/// </summary>
		[Parameter]
		public RenderFragment ChildContent { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The nullable underlying type.
		/// </summary>
		private Type NullableUnderlyingType;
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Nothing to do here.
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

			// Initializations
			this.NullableUnderlyingType = Nullable.GetUnderlyingType(typeof(T));
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
			if (typeof(T) == typeof(string))
			{
				result = (T)(object)value;
				validationErrorMessage = null;
				return true;
			}
			else if (typeof(T).IsEnum || (this.NullableUnderlyingType != null && this.NullableUnderlyingType.IsEnum))
			{
				var success = BindConverter.TryConvertTo<T>(value, CultureInfo.CurrentCulture, out var parsedValue);
				if (success)
				{
					result = parsedValue;
					validationErrorMessage = null;
					return true;
				}
				else
				{
					throw new InvalidOperationException($"{this.GetType()} does not support the value '{typeof(T)}'.");
				}
			}

			throw new InvalidOperationException($"{this.GetType()} does not support the type '{typeof(T)}'.");
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Invoked when the input changes.
		/// </summary>
		/// 
		/// <param name="arguments">The arguments.</param>
		private void OnInputChanges(ChangeEventArgs arguments)
		{
			this.CurrentValueAsString = arguments.Value.ToString();
			this.StateHasChanged();
		}
		#endregion
	}
}