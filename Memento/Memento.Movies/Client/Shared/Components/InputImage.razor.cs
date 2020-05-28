using Blazor.FileReader;
using Memento.Shared.Extensions;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a markdown input for specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputImage : InputBase<string>
	{
		#region [Constants]
		/// <summary>
		/// The default extensions that the input accepts.
		/// </summary>
		private const string DEFAULT_ACCEPTS = ".JPG, .JPEG, .PNG";

		/// <summary>
		/// The default buffer size.
		/// </summary>
		private const int DEFAULT_BUFFER_SIZE = 4 * 1024;
		#endregion

		#region [Properties] Parameters
		/// <summary>
		/// The extensions that the input accepts.
		/// </summary>
		[Parameter]
		public string Accepts { get; set; }

		/// <summary>
		/// The initial image url.
		/// </summary>
		[Parameter]
		public string InitialImageUrl { get; set; }

		/// <summary>
		/// The initial image base64.
		/// </summary>
		[Parameter]
		public string InitialImageBase64 { get; set; }

		/// <summary>
		/// The content to be shown inside the image label.
		/// </summary>
		[Parameter]
		public RenderFragment ImageLabelContent { get; set; }

		/// <summary>
		/// The content to be shown inside the preview label.
		/// </summary>
		[Parameter]
		public RenderFragment PreviewLabelContent { get; set; }
		#endregion

		#region [Properties] Services
		/// <summary>
		/// The file reader service.
		/// </summary>
		[Inject]
		private IFileReaderService FileReader { get; set; }

		/// <summary>
		/// The localizer service.
		/// </summary>
		[Inject]
		private ILocalizerService Localizer { get; set; }
		#endregion

		#region [Properties] References
		/// <summary>
		/// The input.
		/// </summary>
		private ElementReference Input { get; set; }
		#endregion

		#region [Properties] Internal
		/// <summary>
		/// The name for the inputs field.
		/// </summary>
		private string ForName { get; set; }

		/// <summary>
		/// The display name for the inputs field.
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

			if (string.IsNullOrWhiteSpace(this.Accepts))
			{
				this.Accepts = DEFAULT_ACCEPTS;
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

		/// <inheritdoc />
		protected override Boolean TryParseValueFromString(String value, out String result, out String validationErrorMessage)
		{
			result = value;
			validationErrorMessage = string.Empty;

			return true;
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Invoked when the input changes
		/// </summary>
		///
		/// <param name="arguments">The arguments.</param>
		private async Task OnInputChangesAsync(ChangeEventArgs arguments)
		{
			foreach (var file in await this.FileReader.CreateReference(this.Input).EnumerateFilesAsync())
			{
				using (var stream = await file.CreateMemoryStreamAsync(DEFAULT_BUFFER_SIZE))
				{
					// Convert the image into bytes
					var bytes = new byte[stream.Length];
					stream.Read(bytes, 0, (int)stream.Length);

					// Convert the image into base64
					this.CurrentValue = Convert.ToBase64String(bytes);

					// Notify blazor
					this.StateHasChanged();
				}
			}
		}
		#endregion
	}
}