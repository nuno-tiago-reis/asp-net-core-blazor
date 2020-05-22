using Memento.Shared.Models;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models.Persons
{
	/// <summary>
	/// Implements the interface for a 'Person' repository.
	/// Provides methods to interact with the persons (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Person" />
	/// <seealso cref="PersonFilter" />
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="FilterOrderDirection" />
	public sealed class PersonRepository : ModelRepository<Person, PersonFilter, PersonFilterOrderBy, FilterOrderDirection>, IPersonRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonRepository{Person, PersonFilter, PersonFilterOrderBy, FilterOrderDirection}"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="serviceProvider">The services provider.</param>
		/// <param name="logger">The logger.</param>
		public PersonRepository
		(
			MovieContext context,
			ILookupNormalizer lookupNormalizer,
			IServiceProvider serviceProvider,
			ILogger<PersonRepository> logger
		)
		: base(context, lookupNormalizer, serviceProvider, logger)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] IModelRepository
		/// <inheritdoc />
		public async override Task<Person> CreateAsync(Person person)
		{
			return await base.CreateAsync(person);
		}

		/// <inheritdoc />
		public async override Task<Person> UpdateAsync(Person person)
		{
			return await base.UpdateAsync(person);
		}

		/// <inheritdoc />
		public async override Task DeleteAsync(long personId)
		{
			await base.DeleteAsync(personId);
		}

		/// <inheritdoc />
		public async override Task<Person> GetAsync(long personId)
		{
			return await base.GetAsync(personId);
		}

		/// <inheritdoc />
		public async override Task<IPage<Person>> GetAllAsync(PersonFilter personFilter = null)
		{
			return await base.GetAllAsync(personFilter);
		}

		/// <inheritdoc />
		public async override Task<bool> ExistsAsync(long personId)
		{
			return await base.ExistsAsync(personId);
		}
		#endregion

		#region [Methods] IPersonRepository
		#endregion

		#region [Methods] Utility
		/// <inheritdoc />
		protected override void NormalizeModel(Person person)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void ValidateModel(Person person)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void UpdateModel(Person sourcePerson, Person targetPerson)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetCountQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetSimpleQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetDetailedQueryable()
		{
			return null;
		}

		/// <inheritdoc />
		protected override void FilterQueryable(IQueryable<Person> personQueryable, PersonFilter personFilter)
		{
			// Nothing to do here.
		}
		#endregion
	}
}