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