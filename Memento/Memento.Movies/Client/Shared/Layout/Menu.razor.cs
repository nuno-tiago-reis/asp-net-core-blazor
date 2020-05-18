using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Shared.Layout
{
	/// <summary>
	/// Implements the 'Menu' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class Menu : ComponentBase
	{
		#region [Properties] Services
		/// <summary>
		/// The localizer service.
		/// </summary>
		[Inject]
		public ILocalizerService Localizer { get; set; }
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
			// Nothing to do here.
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