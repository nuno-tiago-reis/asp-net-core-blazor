using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a markdown formatted content.
	/// </summary>
	///
	/// <seealso cref="ComponentBase"/>
	public sealed partial class Markdown : ComponentBase
	{
		#region [Properties] Parameters
		/// <summary>
		/// The markdowns content.
		/// </summary>
		[Parameter]
		public string Content { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The markdowns converted content (html).
		/// </summary>
		private string ConvertedContent { get; set; }
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

			// Initializations
			if (!string.IsNullOrWhiteSpace(this.Content))
			{
				this.ConvertedContent = Markdig.Markdown.ToHtml(this.Content);
			}
			else
			{
				this.ConvertedContent = null;
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