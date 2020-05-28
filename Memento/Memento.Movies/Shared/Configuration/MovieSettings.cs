using Memento.Shared.Middleware.DataProtection;
using Memento.Shared.Services.Localization;
using Memento.Shared.Services.Storage;

namespace Memento.Movies.Shared.Configuration
{
	/// <summary>
	/// Implements the 'Movie' settings.
	/// </summary>
	public sealed class MovieSettings
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the data protection options.
		/// </summary>
		public FileSystemDataProtectionOptions DataProtection { get; set; }

		/// <summary>
		/// Gets or sets the storage options.
		/// </summary>
		public AzureStorageOptions Storage { get; set; }

		/// <summary>
		/// Gets or sets the localization options.
		/// </summary>
		public SharedLocalizerOptions Localization { get; set; }

		/// <summary>
		/// Gets or sets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
		#endregion
	}

	/// <summary>
	/// Implements the 'ConnectionStrings' settings.
	/// </summary>
	public sealed class ConnectionStrings
	{
		#region [Properties]
		/// <summary>
		/// The 'DefaultConnection' connection string.
		/// </summary>
		public string DefaultConnection { get; set; }
		#endregion
	}
}