using Memento.Shared.Configuration;
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
		/// Gets or sets the identity server options.
		/// </summary>
		public IdentityServerClientOptions IdentityClientOptions { get; set; }

		/// <summary>
		/// Gets or sets the identity server options.
		/// </summary>
		public IdentityServerResourceOptions IdentityResourceOptions { get; set; }

		/// <summary>
		/// Gets or sets the localization options.
		/// </summary>
		public SharedLocalizerOptions Localization { get; set; }

		/// <summary>
		/// Gets or sets the storage options.
		/// </summary>
		public FileSystemStorageOptions Storage { get; set; }

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