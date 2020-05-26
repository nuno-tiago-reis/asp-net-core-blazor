using Memento.Shared.Models.Repository;

namespace Memento.Movies.Shared.Models.Persons
{
	/// <summary>
	/// Defines an interface for a 'Person' repository.
	/// Provides methods to interact with the movies (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Person" />
	/// <seealso cref="PersonFilter" />
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public interface IPersonRepository : IModelRepository<Person, PersonFilter, PersonFilterOrderBy, FilterOrderDirection>
	{
		#region [Methods] IPersonRepository
		#endregion
	}
}