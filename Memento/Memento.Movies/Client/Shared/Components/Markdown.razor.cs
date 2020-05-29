using Microsoft.AspNetCore.Components;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a markdown formatted content.
	/// </summary>
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
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Nothing to do here.
		}

		/// <inheritdoc />
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