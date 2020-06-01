using Memento.Movies.Client.Pages.Genres;
using Memento.Movies.Client.Pages.Movies;
using Memento.Movies.Client.Pages.Persons;
using Memento.Movies.Client.Shared.Components;
using Memento.Movies.Shared.Resources;

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
			#region [Routes]
			/// <summary>
			/// The home root route.
			/// </summary>
			public const string Root = "/";
			#endregion

			#region [Breadcrumbs]
			/// <summary>
			/// Returns a breadcrumb link representing the root page.
			/// </summary>
			public static BreadcrumbLink GetRootBreadcrumbLink()
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.HOME,
					Url = Root
				};
			}
			#endregion
		}

		/// <summary>
		/// The genre routes.
		/// </summary>
		public static class GenreRoutes
		{
			#region [Routes]
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
			#endregion

			#region [Breadcrumbs]
			/// <summary>
			/// Returns a breadcrumb link representing the root page.
			/// </summary>
			public static BreadcrumbLink GetRootBreadcrumbLink()
			{
				var name = SharedResources.GENRE_PLURAL;

				return new BreadcrumbLink
				{
					Label = string.Format(SharedResources.BREADCRUMB_LIST_LINK_LABEL, name),
					Url = Root
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the create page.
			/// </summary>
			public static BreadcrumbLink GetCreateBreadcrumbLink()
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_CREATE_LINK_LABEL,
					Url = Create
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the detail page.
			/// </summary>
			///
			/// <param name="genreId">The genre id.</param>
			public static BreadcrumbLink GetDetailBreadcrumbLink(long genreId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_DETAIL_LINK_LABEL,
					Url = string.Format(DetailIndexed, genreId)
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the update page.
			/// </summary>
			///
			/// <param name="genreId">The genre id.</param>
			public static BreadcrumbLink GetUpdateBreadcrumbLink(long genreId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_UPDATE_LINK_LABEL,
					Url = string.Format(UpdateIndexed, genreId)
				};
			}
			#endregion
		}

		/// <summary>
		/// The movie routes.
		/// </summary>
		public static class MovieRoutes
		{
			#region [Routes]
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
			#endregion

			#region [Breadcrumbs]
			/// <summary>
			/// Returns a breadcrumb link representing the root page.
			/// </summary>
			public static BreadcrumbLink GetRootBreadcrumbLink()
			{
				var name = SharedResources.MOVIE_PLURAL;

				return new BreadcrumbLink
				{
					Label = string.Format(SharedResources.BREADCRUMB_LIST_LINK_LABEL, name),
					Url = Root
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the create page.
			/// </summary>
			public static BreadcrumbLink GetCreateBreadcrumbLink()
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_CREATE_LINK_LABEL,
					Url = Create
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the detail page.
			/// </summary>
			///
			/// <param name="movieId">The movie id.</param>
			public static BreadcrumbLink GetDetailBreadcrumbLink(long movieId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_DETAIL_LINK_LABEL,
					Url = string.Format(DetailIndexed, movieId)
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the update page.
			/// </summary>
			///
			/// <param name="movieId">The movie id.</param>
			public static BreadcrumbLink GetUpdateBreadcrumbLink(long movieId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_UPDATE_LINK_LABEL,
					Url = string.Format(UpdateIndexed, movieId)
				};
			}
			#endregion
		}

		/// <summary>
		/// The person routes.
		/// </summary>
		public static class PersonRoutes
		{
			#region [Routes]
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
			#endregion

			#region [Breadcrumbs]
			/// <summary>
			/// Returns a breadcrumb link representing the root page.
			/// </summary>
			public static BreadcrumbLink GetRootBreadcrumbLink()
			{
				var name = SharedResources.PERSON_PLURAL;

				return new BreadcrumbLink
				{
					Label = string.Format(SharedResources.BREADCRUMB_LIST_LINK_LABEL, name),
					Url = Root
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the create page.
			/// </summary>
			public static BreadcrumbLink GetCreateBreadcrumbLink()
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_CREATE_LINK_LABEL,
					Url = Create
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the detail page.
			/// </summary>
			///
			/// <param name="personId">The person id.</param>
			public static BreadcrumbLink GetDetailBreadcrumbLink(long personId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_DETAIL_LINK_LABEL,
					Url = string.Format(DetailIndexed, personId)
				};
			}

			/// <summary>
			/// Returns a breadcrumb link representing the update page.
			/// </summary>
			///
			/// <param name="personId">The person id.</param>
			public static BreadcrumbLink GetUpdateBreadcrumbLink(long personId)
			{
				return new BreadcrumbLink
				{
					Label = SharedResources.BREADCRUMB_UPDATE_LINK_LABEL,
					Url = string.Format(UpdateIndexed, personId)
				};
			}
			#endregion
		}
	}
}