namespace Memento.Movies.Shared.Contracts.Genres
{
	/// <summary>
	/// Implements the 'GenreUpdate' contract.
	/// </summary>
	public sealed class GenreUpdateContract
	{
		#region [Properties]
		/// <summary>
		/// TThe Genre's name.
		/// </summary>
		public string Name { get; set; }
		#endregion
	}
}