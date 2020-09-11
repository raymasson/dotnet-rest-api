using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
	{
		public ContactRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public PagedList<Contact> GetContacts(ContactParameters contactParameters)
		{
			var contacts = FindByCondition(c => c.Age >= contactParameters.MinAge &&
										c.Age <= contactParameters.MaxAge);

			return PagedList<Contact>.ToPagedList(
                contacts,
				contactParameters.PageNumber,
				contactParameters.PageSize);
		}
	}
}