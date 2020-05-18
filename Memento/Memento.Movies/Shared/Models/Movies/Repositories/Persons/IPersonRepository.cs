using Memento.Shared.Models.Repositories;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Persons
{
	/// <summary>
	/// Defines an interface for a 'Person' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Person" />
	/// <seealso cref="PersonFilter" />
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="PersonFilterOrderDirection" />
	public interface IPersonRepository : IModelRepository<Person, PersonFilter, PersonFilterOrderBy, PersonFilterOrderDirection>
	{
		#region [Methods] IPersonRepository
		#endregion
	}
}