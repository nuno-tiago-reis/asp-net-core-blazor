using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'PageTable' component.
	/// </summary>
	///
	/// <typeparam name="T">The type.</typeparam>
	///
	/// <seealso cref="MementoComponent{}"/>
	public sealed partial class PageTable<T> : MementoComponent<PageTable<T>>
	{
		#region [Properties]
		/// <summary>
		/// The page.
		/// </summary>
		[Parameter]
		public IPage<T> Page { get; set; }

		/// <summary>
		/// The template to be used for the header.
		/// </summary>
		[Parameter]
		public RenderFragment<T> HeaderTemplate { get; set; }

		/// <summary>
		/// The template to be used for the page items.
		/// </summary>
		[Parameter]
		public RenderFragment<T> ItemTemplate { get; set; }

		/// <summary>
		/// The template to be used when the no results are found.
		/// </summary>
		[Parameter]
		public RenderFragment<T> EmptyTemplate { get; set; }

		/// <summary>
		/// The template to be used when the results are loading.
		/// </summary>
		[Parameter]
		public RenderFragment<T> LoadingTemplate { get; set; }
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
			if (this.ItemTemplate == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.ItemTemplate)} parameter."
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
}