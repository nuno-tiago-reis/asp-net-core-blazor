using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Implements the 'GenericList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class GenericList<T> : ComponentBase
	{
		#region [Properties]
		/// <summary>
		/// The generic element list.
		/// </summary>
		[Parameter]
		public List<T> ElementList { get; set; }

		/// <summary>
		/// The template to be used for the generic elements.
		/// </summary>
		[Parameter]
		public RenderFragment<T> ElementTemplate { get; set; }

		/// <summary>
		/// The template to be used when the list is empty.
		/// </summary>
		[Parameter]
		public RenderFragment EmptyTemplate { get; set; }

		/// <summary>
		/// The template to be used when the list is loading.
		/// </summary>
		[Parameter]
		public RenderFragment LoadingTemplate { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		protected override void OnInitialized()
		{
			// Nothing to do here.

			System.Console.WriteLine("OnInitialized()");
		}

		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(3000);

			System.Console.WriteLine("OnInitializedAsync()");
		}

		/// <inheritdoc />
		protected override void OnParametersSet()
		{
			// Nothing to do here.

			System.Console.WriteLine("OnParametersSet()");
		}

		/// <inheritdoc />
		protected async override Task OnParametersSetAsync()
		{
			await Task.Delay(3000);

			System.Console.WriteLine("OnParametersSetAsync()");
		}

		/// <inheritdoc />
		protected override void OnAfterRender(bool firstRender)
		{
			// Nothing to do here.

			System.Console.WriteLine("OnAfterRender()");
		}

		/// <inheritdoc />
		protected async override Task OnAfterRenderAsync(bool firstRender)
		{
			await Task.Delay(3000);

			System.Console.WriteLine("OnAfterRenderAsync()");
		}

		/// <inheritdoc />
		protected override bool ShouldRender()
		{
			return true;
		}
		#endregion
	}
}