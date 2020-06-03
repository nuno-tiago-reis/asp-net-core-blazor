using Memento.Shared.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'PageListControls' component.
	/// </summary>
	/// 
	/// <typeparam name="TOrderBy">The order by type.</typeparam>
	/// <typeparam name="TOrderDirection">The order direction type.</typeparam>
	/// 
	/// <seealso cref="MementoComponent{PageListControls}"/>
	public sealed partial class PageListControls<TOrderBy, TOrderDirection> : MementoComponent<PageListControls<TOrderBy, TOrderDirection>>
	{
		#region [Properties]
		/// <summary>
		/// The page number.
		/// </summary>
		[Parameter]
		public int PageNumber { get; set; }

		/// <summary>
		/// The page size.
		/// </summary>
		[Parameter]
		public int PageSize { get; set; }

		/// <summary>
		/// The page sizes.
		/// </summary>
		[Parameter]
		public int[] PageSizes { get; set; }

		/// <summary>
		/// The total items.
		/// </summary>
		[Parameter]
		public int TotalItems { get; set; }

		/// <summary>
		/// The total pages.
		/// </summary>
		[Parameter]
		public int TotalPages { get; set; }

		/// <summary>
		/// The order by.
		/// </summary>
		[Parameter]
		public TOrderBy OrderBy { get; set; }

		/// <summary>
		/// The order direction.
		/// </summary>
		[Parameter]
		public TOrderDirection OrderDirection { get; set; }

		/// <summary>
		/// The callback that is invoked when there are page number changes.
		/// </summary>
		[Parameter]
		public EventCallback<int> PageNumberChanged { get; set; }

		/// <summary>
		/// The callback that is invoked when there are page size changes.
		/// </summary>
		[Parameter]
		public EventCallback<int> PageSizeChanged { get; set; }

		/// <summary>
		/// The callback that is invoked when there are order by changes.
		/// </summary>
		[Parameter]
		public EventCallback<TOrderBy> OrderByChanged { get; set; }

		/// <summary>
		/// The callback that is invoked when there are order direction changes.
		/// </summary>
		[Parameter]
		public EventCallback<TOrderDirection> OrderDirectionChanged { get; set; }
		#endregion

		#region [Methods] Component
		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Initializations
			this.PageSizes = new [] { 3, 6, 9, 12 };
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// Validations
			if (this.OrderBy == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.OrderBy)} parameter."
				);
			}

			if (this.OrderDirection == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a value for the {nameof(this.OrderDirection)} parameter."
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

		#region [Methods] Paging
		/// <summary>
		/// Invoked when the page number changes.
		/// </summary>
		///
		/// <param name="pageNumber">The page number.</param>
		private async Task OnPageNumberChangeAsync(int pageNumber)
		{
			await this.PageNumberChanged.InvokeAsync(pageNumber);
		}

		/// <summary>
		/// Invoked when the page size changes.
		/// </summary>
		///
		/// <param name="pageSize">The page size.</param>
		private async Task OnPageSizeChangeAsync(int pageSize)
		{
			await this.PageSizeChanged.InvokeAsync(pageSize);
		}
		#endregion

		#region [Methods] Ordering
		/// <summary>
		/// Invoked when the order by changes.
		/// </summary>
		///
		/// <param name="orderBy">The order by.</param>
		private async Task OnOrderByChangeAsync(TOrderBy orderBy)
		{
			await this.OrderByChanged.InvokeAsync(orderBy);
		}

		/// <summary>
		/// Invoked when the order direction changes.
		/// </summary>
		///
		/// <param name="orderDirection">The order direction.</param>
		private async Task OnOrderDirectionChangeAsync(TOrderDirection orderDirection)
		{
			await this.OrderDirectionChanged.InvokeAsync(orderDirection);
		}
		#endregion
	}
}