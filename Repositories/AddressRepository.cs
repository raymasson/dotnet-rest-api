using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
	{
		public AddressRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{

		}

		public List<FullContact> GetFullContacts()
		{
			return this.RepositoryContext.Contacts.Join(
                this.RepositoryContext.Addresses,
                contact => contact.Id,
                address => address.ContactId,
                (contact, address) => new FullContact
                {
                    ZipCode = address.ZipCode,
                    City = address.City,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName
                }   
            ).ToList();
		}
	}
}