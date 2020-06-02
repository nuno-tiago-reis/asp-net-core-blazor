using Memento.Shared.Components;
using Memento.Shared.Models.Pagination;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'PageList' component.
	/// </summary>
	///
	/// <typeparam name="T">The type.</typeparam>
	///
	/// <seealso cref="ComponentBase"/>
	public sealed partial class PageList<T> : MementoComponent<PageList<T>>
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
			if (this.ItemTemplate == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.ItemTemplate)} parameter."
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
	}
}