using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'PaginatedList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PaginatedList<T> : ComponentBase
	{
		#region [Properties]
		/// <summary>
		/// The page.
		/// </summary>
		[Parameter]
		public IPage<T> Page { get; set; }

		/// <summary>
		/// The template to be used for the page items.
		/// </summary>
		[Parameter]
		public RenderFragment<T> PageItemTemplate { get; set; }

		/// <summary>
		/// The template to be used when the page is empty.
		/// </summary>
		[Parameter]
		public RenderFragment<T> PageEmptyTemplate { get; set; }

		/// <summary>
		/// The template to be used when the page is loading.
		/// </summary>
		[Parameter]
		public RenderFragment<T> PageLoadingTemplate { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void OnParametersSet()
		{
			if (this.PageItemTemplate == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.PageItemTemplate)} parameter."
				);
			}
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