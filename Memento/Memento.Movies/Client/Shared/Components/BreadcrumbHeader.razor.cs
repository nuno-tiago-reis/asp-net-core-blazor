using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'BreadcrumbHeader' component.
	/// </summary>
	///
	/// <seealso cref="MementoComponent{}"/>
	public sealed partial class BreadcrumbHeader : MementoComponent<BreadcrumbHeader>
	{
		#region [Properties]
		/// <summary>
		/// The primary header.
		/// </summary>
		[Parameter]
		public string PrimaryHeader { get; set; }

		/// <summary>
		/// The secondary header.
		/// </summary>
		[Parameter]
		public string SecondaryHeader { get; set; }

		/// <summary>
		/// The links.
		/// </summary>
		[Parameter]
		public IList<BreadcrumbLink> Links { get; set; }

		/// <summary>
		/// The actions.
		/// </summary>
		[Parameter]
		public IList<BreadcrumbAction> Actions { get; set; }
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
			if (string.IsNullOrWhiteSpace(this.PrimaryHeader))
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.PrimaryHeader)} parameter."
				);
			}

			if (string.IsNullOrWhiteSpace(this.SecondaryHeader))
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.SecondaryHeader)} parameter."
				);
			}

			if (this.Links == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.Links)} parameter."
				);
			}

			if (this.Actions == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.Actions)} parameter."
				);
			}
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

	/// <summary>
	/// Implements the 'BreadcrumbAction' model.
	/// BreadcrumbActions are used to display action buttons on the right side of a 'Breadcrumb'.
	/// </summary>
	///
	/// <seealso cref="BreadcrumbHeader"/>
	public sealed class BreadcrumbAction
	{
		/// <summary>
		/// The icon (css class).
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// The label.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// The label.
		/// </summary>
		public string Tooltip { get; set; }

		/// <summary>
		/// The enabled flag.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// The callback to be invoked when the button is clicked.
		/// </summary>
		public EventCallback<MouseEventArgs> OnClick { get; set; }
	}

	/// <summary>
	/// Implements the 'BreadcrumbAction' model.
	/// BreadcrumbActions are used to display navigation links on the left side of a 'Breadcrumb'.
	/// </summary>
	///
	/// <seealso cref="BreadcrumbHeader"/>
	public sealed class BreadcrumbLink
	{
		/// <summary>
		/// The label.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// The url.
		/// </summary>
		public string Url { get; set; }
	}
}