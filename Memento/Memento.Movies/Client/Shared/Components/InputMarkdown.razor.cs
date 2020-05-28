using Memento.Shared.Extensions;
using Memento.Shared.Services.Localization;
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
	/// Displays a markdown input for specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputMarkdown : InputTextArea
	{
		#region [Properties] Parameters
		/// <summary>
		/// The content to be shown inside the markdown label.
		/// </summary>
		[Parameter]
		public RenderFragment MarkdownLabelContent { get; set; }

		/// <summary>
		/// The content to be shown inside the preview label.
		/// </summary>
		[Parameter]
		public RenderFragment PreviewLabelContent { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The localizer service.
		/// </summary>
		[Inject]
		private ILocalizerService Localizer { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The name for the markdowns field.
		/// </summary>
		private string ForName { get; set; }

		/// <summary>
		/// The display name for the markdowns field.
		/// </summary>
		private string ForDisplayName { get; set; }
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
			if (this.EditContext == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a cascading parameter of type {nameof(EditContext)}. " +
					$"For example, you can use {this.GetType()} inside an {nameof(EditForm)}."
				);
			}

			var property = ((MemberExpression)this.ValueExpression.Body).Member;
			var propertyDisplayName = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

			this.ForName = ((MemberExpression)this.ValueExpression.Body).Member.Name;
			this.ForDisplayName = propertyDisplayName?.GetName() ?? this.ForName.SpacesFromCamel();
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