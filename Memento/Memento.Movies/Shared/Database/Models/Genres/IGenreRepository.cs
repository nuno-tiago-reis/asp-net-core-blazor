﻿using Memento.Shared.Models;

namespace Memento.Movies.Shared.Database.Models.Genres
{
	/// <summary>
	/// Defines an interface for a 'Genre' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Genre" />
	/// <seealso cref="GenreFilter" />
	/// <seealso cref="GenreFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public interface IGenreRepository : IModelRepository<Genre, GenreFilter, GenreFilterOrderBy, FilterOrderDirection>
	{
		#region [Methods] IGenreRepository
		#endregion
	}
}