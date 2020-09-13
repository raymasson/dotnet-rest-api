using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Linq.Expressions;

namespace Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
	{
		private ISortHelper<Contact> _sortHelper;

		public ContactRepository(RepositoryContext repositoryContext, ISortHelper<Contact> sortHelper)
			: base(repositoryContext)
		{
			_sortHelper = sortHelper;
		}

		public PagedList<Contact> GetContacts(ContactParameters contactParameters)
		{
			// Build dynamically the lambda expression for filters
			var parameter = Expression.Parameter(typeof(Contact), "c");

			// Age filter
			var minAgeConstant = Expression.Constant(contactParameters.MinAge);
			var maxAgeConstant = Expression.Constant(contactParameters.MaxAge);
			var ageProperty = Expression.Property(parameter, "Age");
			var minAgeExpression = Expression.GreaterThanOrEqual(ageProperty, minAgeConstant);
			var maxAgeExpression = Expression.LessThanOrEqual(ageProperty, maxAgeConstant);
			var expression = Expression.And(minAgeExpression, maxAgeExpression);
			
			// Gender filter
			if (!String.IsNullOrEmpty(contactParameters.Gender)) {
				var genderProperty = Expression.Property(parameter, "Gender"); 
				var genderConstant = Expression.Constant(contactParameters.Gender);
				var genderExpression = Expression.Equal(genderProperty, genderConstant);
				expression = Expression.And(expression, genderExpression);
			}

			// Find contacts
			var contacts = FindByCondition( Expression.Lambda<Func<Contact,bool>>(expression, parameter));

			// Sort contacts
			var sortedContacts = _sortHelper.ApplySort(contacts, contactParameters.SortBy);

			return PagedList<Contact>.ToPagedList(
                sortedContacts,
				contactParameters.PageNumber,
				contactParameters.PageSize);
		}
	}
}