using Memento.Movies.Shared.Resources;
using Memento.Shared.Exceptions;
using Memento.Shared.Extensions;
using Memento.Shared.Models.Pagination;
using Memento.Shared.Models.Repositories;
using Memento.Shared.Services.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Memento.Movies.Shared.Models.Movies.Repositories.Persons
{
	/// <summary>
	/// Implements the interface for a 'Person' repository.
	/// Provides methods to interact with the persons (CRUD and more).
	/// </summary>
	///
	/// <seealso cref="Person" />
	/// <seealso cref="PersonFilter" />
	/// <seealso cref="PersonFilterOrderBy" />
	/// <seealso cref="PersonFilterOrderDirection" />
	public sealed class PersonRepository : ModelRepository<Person, PersonFilter, PersonFilterOrderBy, PersonFilterOrderDirection>, IPersonRepository
	{
		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context</param>
		/// <param name="localizer">The localizer.</param>
		/// <param name="lookupNormalizer">The lookup normalizer.</param>
		/// <param name="logger">The logger.</param>
		public PersonRepository
		(
			MovieContext context,
			ILocalizerService localizer,
			ILookupNormalizer lookupNormalizer,
			ILogger<PersonRepository> logger
		)
		: base(context, localizer, lookupNormalizer, logger)
		{
			// Nothing to do here.
		}
		#endregion

		#region [Methods] IModelRepository
		/// <inheritdoc />
		public override async Task<Person> CreateAsync(Person person)
		{
			return await base.CreateAsync(person);
		}

		/// <inheritdoc />
		public override async Task<Person> UpdateAsync(Person person)
		{
			return await base.UpdateAsync(person);
		}

		/// <inheritdoc />
		public override async Task DeleteAsync(long personId)
		{
			await base.DeleteAsync(personId);
		}

		/// <inheritdoc />
		public override async Task<Person> GetAsync(long personId)
		{
			return await base.GetAsync(personId);
		}

		/// <inheritdoc />
		public override async Task<IPage<Person>> GetAllAsync(PersonFilter personFilter = null)
		{
			return await base.GetAllAsync(personFilter);
		}

		/// <inheritdoc />
		public override async Task<bool> ExistsAsync(long personId)
		{
			return await base.ExistsAsync(personId);
		}
		#endregion

		#region [Methods] IPersonRepository
		#endregion

		#region [Methods] Model
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
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(person => person.Name));
			}
			if (string.IsNullOrWhiteSpace(sourcePerson.Biography))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(person => person.Biography));
			}
			if (string.IsNullOrWhiteSpace(sourcePerson.PictureUrl))
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(person => person.PictureUrl));
			}
			if (sourcePerson.BirthDate == default)
			{
				errorMessages.Add(this.GetModelHasInvalidFieldMessage(person => person.BirthDate));
			}

			// Duplicate fields
			if (this.Models.Any(person => person.Id != sourcePerson.Id && person.NormalizedName.Equals(sourcePerson.NormalizedName) && person.BirthDate == sourcePerson.BirthDate))
			{
				errorMessages.Add(this.GetModelHasDuplicateFieldMessage(person => person.Name));
				errorMessages.Add(this.GetModelHasDuplicateFieldMessage(person => person.BirthDate));
			}

			if (errorMessages.Count > 0)
			{
				throw new MementoException(errorMessages, MementoExceptionType.BadRequest);
			}
		}

		/// <inheritdoc />
		protected override void UpdateModel(Person sourcePerson, Person targetPerson)
		{
			// Person
			targetPerson.Name = sourcePerson.Name;
			targetPerson.NormalizedName = sourcePerson.NormalizedName;
			targetPerson.Biography = sourcePerson.Biography;
			targetPerson.PictureUrl = sourcePerson.PictureUrl;
			targetPerson.BirthDate = sourcePerson.BirthDate;

			// Model
			targetPerson.CreatedAt = sourcePerson.CreatedAt;
			targetPerson.CreatedBy = sourcePerson.CreatedBy;
			targetPerson.UpdatedAt = sourcePerson.UpdatedAt;
			targetPerson.UpdatedBy = sourcePerson.UpdatedBy;
		}

		/// <inheritdoc />
		protected override void UpdateModelRelations(Person sourcePerson, Person targetPerson)
		{
			// Check which movies need to be added
			if (sourcePerson.Movies != null)
			{
				// Clear duplicates
				sourcePerson.Movies = sourcePerson.Movies
					.GroupBy(movie => new { movie.MovieId, movie.Role }, (key, movies) => movies.FirstOrDefault())
					.ToList();

				// Cross check the source with the target
				foreach (var sourceMovie in sourcePerson.Movies)
				{
					if (targetPerson.Movies == null || targetPerson.Movies.All(movie => movie.MovieId != sourceMovie.MovieId || movie.Role != sourceMovie.Role))
					{
						// Make sure the link is there
						sourceMovie.PersonId = targetPerson.Id;

						// Add the connection to the context
						this.Context.Add(sourceMovie);
						continue;
					}
				}
			}

			// Check which movies need to be removed
			if (targetPerson.Movies != null)
			{
				// Cross check the source with the target
				foreach (var targetMovie in targetPerson.Movies)
				{
					if (sourcePerson.Movies == null || sourcePerson.Movies.All(movie => movie.MovieId != targetMovie.MovieId && movie.Role != targetMovie.Role))
					{
						// Remove the connection from the context
						this.Context.Remove(targetMovie);
						continue;
					}
				}
			}
		}
		#endregion

		#region [Methods] Queryable
		/// <inheritdoc />
		protected override IQueryable<Person> GetCountQueryable()
		{
			return this.Models;
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetSimpleQueryable()
		{
			return this.Models
				.Include(person => person.Movies)
				.ThenInclude(personMovies => personMovies.Movie);
		}

		/// <inheritdoc />
		protected override IQueryable<Person> GetDetailedQueryable()
		{
			return this.Models
				.Include(person => person.Movies)
				.ThenInclude(personMovies => personMovies.Movie);
		}

		/// <inheritdoc />
		protected override IQueryable<Person> FilterQueryable(IQueryable<Person> personQueryable, PersonFilter personFilter)
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
					personQueryable = OrderQueryable(personQueryable, personFilter, person => person.Id);
					break;
				}

				case PersonFilterOrderBy.Name:
				{
					personQueryable = OrderQueryable(personQueryable, personFilter, person => person.Name);
					break;
				}

				case PersonFilterOrderBy.BirthDate:
				{
					personQueryable = OrderQueryable(personQueryable, personFilter, person => person.BirthDate);
					break;
				}

				case PersonFilterOrderBy.CreatedAt:
				{
					personQueryable = OrderQueryable(personQueryable, personFilter, person => person.CreatedAt);
					break;
				}

				case PersonFilterOrderBy.UpdatedAt:
				{
					personQueryable = OrderQueryable(personQueryable, personFilter, person => person.UpdatedAt);
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException(nameof(personFilter.OrderBy));
				}
			}

			return personQueryable;
		}

		/// <summary>
		/// Returns an ordered queryable according to the filters OrderDirection and expressions Property.
		/// </summary>
		///
		/// <typeparam name="TProperty">The property's type.</typeparam>
		///
		/// <param name="personQueryable">The person queryable.</param>
		/// <param name="personFilter">The person filter.</param>
		/// <param name="personExpression">The person expression</param>
		private static IQueryable<Person> OrderQueryable<TProperty>(IQueryable<Person> personQueryable, PersonFilter personFilter, Expression<Func<Person, TProperty>> personExpression)
		{
			switch (personFilter.OrderDirection)
			{
				case PersonFilterOrderDirection.Ascending:
				{
					return personQueryable.OrderBy(personExpression);
				}
				case PersonFilterOrderDirection.Descending:
				{
					return personQueryable.OrderByDescending(personExpression);
				}
				default:
				{
					throw new ArgumentOutOfRangeException(nameof(personFilter.OrderDirection));
				}
			}
		}
		#endregion

		#region [Methods] Messages
		/// <inheritdoc />
		protected override string GetModelDoesNotExistMessage()
		{
			// Get the name
			var name = this.Localizer.GetString(SharedResources.PERSON);

			return this.Localizer.GetString(SharedResources.ERROR_NOT_FOUND, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasDuplicateFieldMessage<TProperty>(Expression<Func<Person, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_DUPLICATE_FIELD, name);
		}

		/// <inheritdoc />
		protected override string GetModelHasInvalidFieldMessage<TProperty>(Expression<Func<Person, TProperty>> expression)
		{
			// Get the name
			var name = expression.GetDisplayName();

			return this.Localizer.GetString(SharedResources.ERROR_INVALID_FIELD, name);
		}
		#endregion
	}
}