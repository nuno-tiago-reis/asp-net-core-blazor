namespace Memento.Movies.Server.Shared.Routes
{
	/// <summary>
	/// Defines all the available routes.
	/// </summary>
	public static class Routes
	{
		/// <summary>
		/// The genre routes.
		/// </summary>
		public static class GenreRoutes
		{
			/// <summary>
			/// The genres root route.
			/// </summary>
			public const string Root = "/Api/Genres/";
		}

		/// <summary>
		/// The movie routes.
		/// </summary>
		public static class MovieRoutes
		{
			/// <summary>
			/// The movies root route.
			/// </summary>
			public const string Root = "/Api/Movies/";
		}

		/// <summary>
		/// The person routes.
		/// </summary>
		public static class PersonRoutes
		{
			/// <summary>
			/// The persons root route.
			/// </summary>
			public const string Root = "/Api/Persons/";
		}
	}
}