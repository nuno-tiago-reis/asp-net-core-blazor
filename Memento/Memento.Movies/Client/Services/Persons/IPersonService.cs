using Memento.Movies.Shared.Contracts.Persons;
using Memento.Movies.Shared.Models.Persons;
using Memento.Shared.Pagination;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Services.Persons
{
	/// <summary>
	/// Defines an interface for an API person service.
	/// Provides methods to interact with the API (CRUD and more).
	/// </summary>
	public interface IPersonService
	{
		#region [Methods]
		/// <summary>
		/// Invokes the API to create the given 'Person'.
		/// </summary>
		/// 
		/// <param name="person">The person.</param>
		Task<PersonDetailContract> CreateAsync(PersonCreateContract person);

		/// <summary>
		/// Invokes the API to update the given 'Person'.
		/// </summary>
		/// 
		/// <param name="personId">The person identifier.</param>
		/// <param name="person">The person.</param>
		Task UpdateAsync(long personId, PersonUpdateContract person);

		/// <summary>
		/// Invokes the API to delete the 'Person' with the given identifier.
		/// </summary>
		/// 
		/// <param name="personId">The person identifier.</param>
		Task DeletePerson(long personId);

		/// <summary>
		/// Invokes the API to get the 'Person' with the given identifier.
		/// </summary>
		/// 
		/// <param name="personId">The person identifier.</param>
		Task<PersonDetailContract> GetAsync(long personId);

		/// <summary>
		/// Invokes the API to get the 'Persons' according to the given filter.
		/// </summary>
		/// 
		/// <param name="personFilter">The person filter.</param>
		Task<Page<PersonListContract>> GetAllAsync(PersonFilter personFilter = null);
		#endregion
	}
}