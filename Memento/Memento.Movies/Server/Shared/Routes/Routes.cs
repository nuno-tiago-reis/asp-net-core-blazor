namespace Memento.Movies.Server.Shared.Routes
{
	/// <summary>
	/// Defines all the available routes.
	/// </summary>
	public static class Routes
	{
		#region [Movies]
		/// <summary>
		/// The genre routes.
		/// </summary>
		public static class GenreRoutes
		{
			/// <summary>
			/// The genres root route.
			/// </summary>
			public const string ROOT = "/Api/Genres/";
		}

		/// <summary>
		/// The movie routes.
		/// </summary>
		public static class MovieRoutes
		{
			/// <summary>
			/// The movies root route.
			/// </summary>
			public const string ROOT = "/Api/Movies/";

			/// <summary>
			/// The movies 'InTheaters' route.
			/// </summary>
			public const string IN_THEATERS = "/Api/Movies/In-Theaters";

			/// <summary>
			/// The movies 'UpcomingReleases' route.
			/// </summary>
			public const string UPCOMING_RELEASES = "/Api/Movies/Upcoming-Releases";
		}

		/// <summary>
		/// The person routes.
		/// </summary>
		public static class PersonRoutes
		{
			/// <summary>
			/// The persons root route.
			/// </summary>
			public const string ROOT = "/Api/Persons/";
		}
		#endregion
	}
}