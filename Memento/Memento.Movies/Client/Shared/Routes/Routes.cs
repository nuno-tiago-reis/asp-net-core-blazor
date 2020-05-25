using Memento.Movies.Client.Pages.Genres;
using Memento.Movies.Client.Pages.Movies;
using Memento.Movies.Client.Pages.Persons;

namespace Memento.Movies.Client.Shared.Routes
{
	/// <summary>
	/// Defines all the available routes.
	/// </summary>
	public static class Routes
	{
		/// <summary>
		/// The home routes.
		/// </summary>
		public static class HomeRoutes
		{
			/// <summary>
			/// The home root route.
			/// </summary>
			public const string Root = "/";
		}

		/// <summary>
		/// The genre routes.
		/// </summary>
		public static class GenreRoutes
		{
			/// <summary>
			/// The genres root route.
			/// </summary>
			public const string Root = "/Genres/";

			/// <summary>
			/// The genres create route.
			/// </summary>
			public const string Create = "/Genres/Create";

			/// <summary>
			/// The genres detail route.
			/// </summary>
			public const string Detail = "/Genres/{" + nameof(GenreDetail.GenreId) + ":long}";

			/// <summary>
			/// The genres detail route (indexed).
			/// </summary>
			public const string DetailIndexed = "/Genres/{0}";

			/// <summary>
			/// The genres update route.
			/// </summary>
			public const string Update = "/Genres/{" + nameof(GenreForm.GenreId) + ":long}/Update";

			/// <summary>
			/// The genres update route (indexed).
			/// </summary>
			public const string UpdateIndexed = "/Genres/{0}/Update";
		}

		/// <summary>
		/// The movie routes.
		/// </summary>
		public static class MovieRoutes
		{
			/// <summary>
			/// The movies root route.
			/// </summary>
			public const string Root = "/Movies/";

			/// <summary>
			/// The movies create route.
			/// </summary>
			public const string Create = "/Movies/Create";

			/// <summary>
			/// The movies detail route.
			/// </summary>
			public const string Detail = "/Movies/{" + nameof(MovieDetail.MovieId) + ":long}";

			/// <summary>
			/// The movies detail route (indexed).
			/// </summary>
			public const string DetailIndexed = "/Movies/{0}";

			/// <summary>
			/// The movies update route.
			/// </summary>
			public const string Update = "/Movies/{" + nameof(MovieForm.MovieId) + ":long}/Update";

			/// <summary>
			/// The movies update route (indexed).
			/// </summary>
			public const string UpdateIndexed = "/Movies/{0}/Update";
		}

		/// <summary>
		/// The person routes.
		/// </summary>
		public static class PersonRoutes
		{
			/// <summary>
			/// The persons root route.
			/// </summary>
			public const string Root = "/Persons/";

			/// <summary>
			/// The persons create route.
			/// </summary>
			public const string Create = "/Persons/Create";

			/// <summary>
			/// The persons detail route.
			/// </summary>
			public const string Detail = "/Persons/{" + nameof(PersonDetail.PersonId) + ":long}";

			/// <summary>
			/// The persons detail route (indexed).
			/// </summary>
			public const string DetailIndexed = "/Persons/{0}";

			/// <summary>
			/// The persons update route.
			/// </summary>
			public const string Update = "/Persons/{" + nameof(PersonForm.PersonId) + ":long}/Update";

			/// <summary>
			/// The persons update route (indexed).
			/// </summary>
			public const string UpdateIndexed = "/Persons/{0}/Update";
		}
	}
}