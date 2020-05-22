namespace Memento.Movies.Shared.Contracts.Genres
{
	/// <summary>
	/// Implements the 'GenreCreate' contract.
	/// </summary>
	public sealed class GenreCreateContract
	{
		#region [Properties]
		/// <summary>
		/// The Genre's name.
		/// </summary>
		public string Name { get; set; }
		#endregion
	}
}