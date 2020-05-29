using Memento.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a label for a specified field within a cascaded <see cref="Microsoft.AspNetCore.Components.Forms.EditContext"/>.
	/// </summary>
	public sealed partial class Label<T> : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The edit context to which the label belongs.
		/// </summary>
		[CascadingParameter]
		public EditContext EditContext { get; set; }

		/// <summary>
		/// The content to be shown inside the label.
		/// </summary>
		[Parameter]
		public RenderFragment Content { get; set; }

		/// <summary>
		/// The field for which the label should be displayed.
		/// </summary>
		[Parameter]
		public Expression<Func<T>> For { get; set; }

		/// <summary>
		/// Gets or sets a collection of additional attributes that will be applied to the created <c>div</c> element.
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)]
		public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The name for the labels field.
		/// </summary>
		private string ForName { get; set; }

		/// <summary>
		/// The display name for the labels field.
		/// </summary>
		private string ForDisplayName { get; set; }
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

			if (this.For == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.For)} parameter."
				);
			}

			// Initializations
			this.ForName = this.For.Body.GetName();
			this.ForDisplayName = this.For.Body.GetDisplayName();
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
		#endregion
	}
}