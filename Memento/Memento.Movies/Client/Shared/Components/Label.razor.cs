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
	/// Displays a label for a specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class Label<T> : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// Specifies the edit context to which the label belongs.
		/// </summary>
		[CascadingParameter]
		public EditContext LabelEditContext { get; set; }

		/// <summary>
		/// Specifies the content to be shown inside the label.
		/// </summary>
		[Parameter]
		public RenderFragment LabelContent { get; set; }

		/// <summary>
		/// Specifies the field for which the label should be displayed.
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
		/// Specifies the name.
		/// </summary>
		private string LabelName { get; set; }

		/// <summary>
		/// Specifies the display name.
		/// </summary>
		private string LabelDisplayName { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void OnParametersSet()
		{
			if (this.LabelEditContext == null)
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
					$"{this.GetType()} requires a value for the " + $"{nameof(this.For)} parameter."
				);
			}

			var property = ((MemberExpression)this.For.Body).Member;
			var propertyDisplayName = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

			this.LabelName = ((MemberExpression)this.For.Body).Member.Name;
			this.LabelDisplayName = propertyDisplayName?.Name ?? this.LabelName.SpacesFromCamel();
		}

		/// <inheritdoc />
		protected override void OnAfterRender(bool firstRender)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override bool ShouldRender()
		{
			return true;
		}
		#endregion
	}
}