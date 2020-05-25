using Memento.Shared.Configuration;

namespace Memento.Movies.Shared.Configuration
{
	/// <summary>
	/// Implements the 'Movie' settings.
	/// </summary>
	public sealed class MovieSettings : ApplicationSettings
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the 'FileSystemDataProtection' settings.
		/// </summary>
		public FileSystemDataProtectionSettings DataProtection { get; set; }

		/// <summary>
		/// Gets or sets the 'ReCaptcha' settings.
		/// </summary>
		public GoogleReCaptchaSettings ReCaptcha { get; set; }

		/// <summary>
		/// Gets or sets the 'FileSystemStorage' settings.
		/// </summary>
		public FileSystemStorageSettings Storage { get; set; }

		/// <summary>
		/// The connection strings.
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