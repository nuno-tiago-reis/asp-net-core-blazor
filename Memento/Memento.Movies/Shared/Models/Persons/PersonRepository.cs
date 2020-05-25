using Memento.Shared.Exceptions;
using Memento.Shared.Extensions;
using Memento.Shared.Models;
using Memento.Shared.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		protected override void NormalizeModel(Person sourcePerson)
		{
			sourcePerson.NormalizedName = this.LookupNormalizer.NormalizeName(sourcePerson.Name ?? string.Empty);
		}

		/// <inheritdoc />
		protected override void ValidateModel(Person sourcePerson)
		{
			var errorMessages = new List<string>();

			// Required fields
			if (string.IsNullOrWhiteSpace(sourcePerson.Name))
			{
				errorMessages.Add(sourcePerson.InvalidFieldMessage(person => person.Name));
			}
			if (string.IsNullOrWhiteSpace(sourcePerson.Biography))
			{
				errorMessages.Add(sourcePerson.InvalidFieldMessage(person => person.Biography));
			}
			if (string.IsNullOrWhiteSpace(sourcePerson.PictureUrl))
			{
				errorMessages.Add(sourcePerson.InvalidFieldMessage(person => person.PictureUrl));
			}
			if (sourcePerson.BirthDate == default)
			{
				errorMessages.Add(sourcePerson.InvalidFieldMessage(person => person.BirthDate));
			}

			// Duplicate fields
			if (this.Models.Any(person => person.NormalizedName.Equals(sourcePerson.NormalizedName) && person.BirthDate == sourcePerson.BirthDate))
			{
				errorMessages.Add(sourcePerson.ExistingFieldMessage(person => person.Name));
				errorMessages.Add(sourcePerson.ExistingFieldMessage(person => person.BirthDate));
			}

			if (errorMessages.Count > 0)
			{
				throw new MementoException(errorMessages, MementoExceptionType.BadRequest);
			}
		}

		/// <inheritdoc />
		protected override void UpdateModel(Person sourcePerson, Person targetPerson)
		{
			targetPerson.Name = sourcePerson.Name;
			targetPerson.NormalizedName = sourcePerson.NormalizedName;
			targetPerson.Biography = sourcePerson.Biography;
			targetPerson.PictureUrl = sourcePerson.PictureUrl;
			targetPerson.BirthDate = sourcePerson.BirthDate;
			targetPerson.Movies = sourcePerson.Movies;
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetCountQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetSimpleQueryable()
		{
			return this.Models
				.Include(person => person.Movies);
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetDetailedQueryable()
		{
			return this.Models
				.Include(person => person.Movies);
		}

		/// <inheritdoc />
		protected override void FilterQueryable(IQueryable<Person> personQueryable, PersonFilter personFilter)
		{
			// Apply the filter
			if (string.IsNullOrWhiteSpace(personFilter.Name) == false)
			{
				var name = this.LookupNormalizer.NormalizeName(personFilter.Name);

				personQueryable = personQueryable.Where(person => EF.Functions.Like(person.Name, $"%{name}%"));
			}

			if (string.IsNullOrWhiteSpace(personFilter.Biography) == false)
			{
				var biography = personFilter.Biography;

				personQueryable = personQueryable.Where(person => EF.Functions.Like(person.Biography, $"%{biography}%"));
			}

			if (personFilter.BornAfter != null)
			{
				var birthDate = personFilter.BornAfter.Value;

				personQueryable = personQueryable.Where(person => person.BirthDate >= birthDate);
			}

			if (personFilter.BornBefore != null)
			{
				var birthDate = personFilter.BornBefore.Value;

				personQueryable = personQueryable.Where(person => person.BirthDate <= birthDate);
			}

			// Apply the order
			switch (personFilter.OrderBy)
			{
				case PersonFilterOrderBy.Id:
				{
					personQueryable = personFilter.OrderDirection == FilterOrderDirection.Ascending
						? personQueryable.OrderBy(person => person.Id)
						: personQueryable.OrderByDescending(person => person.Id);
					break;
				}

				case PersonFilterOrderBy.Name:
				{
					personQueryable = personFilter.OrderDirection == FilterOrderDirection.Ascending
						? personQueryable.OrderBy(person => person.Name)
						: personQueryable.OrderByDescending(person => person.Name);
					break;
				}

				case PersonFilterOrderBy.CreatedAt:
				{
					personQueryable = personFilter.OrderDirection == FilterOrderDirection.Ascending
						? personQueryable.OrderBy(person => person.CreatedAt)
						: personQueryable.OrderByDescending(person => person.CreatedAt);
					break;
				}

				case PersonFilterOrderBy.UpdatedAt:
				{
					personQueryable = personFilter.OrderDirection == FilterOrderDirection.Ascending
						? personQueryable.OrderBy(person => person.UpdatedAt)
						: personQueryable.OrderByDescending(person => person.UpdatedAt);
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException(nameof(personFilter.OrderBy));
				}
			}
		}
		#endregion
	}
}