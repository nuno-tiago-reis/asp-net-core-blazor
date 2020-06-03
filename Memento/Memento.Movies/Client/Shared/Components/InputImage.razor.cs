using Blazor.FileReader;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Files;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Components
{
	/// <summary>
	/// Displays a markdown input for specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public sealed partial class InputImage : InputBase<File>
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
		/// The image url.
		/// </summary>
		[Parameter]
		public string ImageUrl { get; set; }

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
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnInitialized()
		{
			base.OnInitialized();

			// Initialize the file restrictions
			this.Accepts = DEFAULT_ACCEPTS;
		}

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// Validations
			if (this.EditContext == null)
			{
				throw new InvalidOperationException
				(
					$"{this.GetType()} requires a cascading parameter of type {nameof(this.EditContext)}. " +
					$"For example, you can use {this.GetType()} inside an {nameof(EditForm)}."
				);
			}

			// Initializations
			this.ForName = this.ValueExpression.GetName();
			this.ForDisplayName = this.ValueExpression.GetDisplayName();
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

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "RedundantOverriddenMember")]
		protected override bool TryParseValueFromString(string value, out File result, out string validationErrorMessage)
		{
			result = JsonSerializer.Deserialize<File>(value);
			validationErrorMessage = string.Empty;

			return false;
		}
		#endregion

		#region [Methods] Events
		/// <summary>
		/// Invoked when the inputs value changes.
		/// </summary>
		///
		/// <param name="arguments">The arguments.</param>
		private async Task OnInputChangeAsync(ChangeEventArgs arguments)
		{
			var fileReader = this.FileReader.CreateReference(this.Input);

			foreach (var file in await fileReader.EnumerateFilesAsync())
			{
				// Open a read stream
				var stream = await file.CreateMemoryStreamAsync(DEFAULT_BUFFER_SIZE);
	
				// Get the file info
				var fileInfo = await file.ReadFileInfoAsync();
				// Get the file bytes
				var fileBytes = new byte[stream.Length];
				stream.Read(fileBytes, 0, (int)stream.Length);
				// Get the file content type
				var provider = new FileExtensionContentTypeProvider();
				if (!provider.TryGetContentType(fileInfo.Name, out var fileContentType))
				{
					fileContentType = "application/octet-stream";
				}

				// Change the state
				this.CurrentValue = new File
				{
					FileBase64 = Convert.ToBase64String(fileBytes),
					FileName = fileInfo.Name,
					FileContentType = fileContentType
				};
				this.StateHasChanged();
			}
		}
		#endregion
	}
}