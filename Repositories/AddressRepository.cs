using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
            // INNER JOIN
			/*return this.RepositoryContext.Contacts.Join(
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
            ).ToList();*/

            // LEFT JOIN
            /*return this.RepositoryContext.Contacts
                    .GroupJoin(
                        this.RepositoryContext.Addresses,
                        contact => contact.Id,
                        address => address.ContactId,
                        (contact, address) => new { contact, address }
                    )
                    .SelectMany(
                        x => x.address.DefaultIfEmpty(), 
                        (x, address) => new FullContact {
                            FirstName = x.contact.FirstName,
                            LastName = x.contact.LastName,
                            City = address == null ? string.Empty : address.City,
                            ZipCode = address == null ? 0 : address.ZipCode
                        }
                    ).ToList();*/

            // Query syntax
            var query = from contact in this.RepositoryContext.Contacts
                              join address in this.RepositoryContext.Addresses
                              on contact.Id equals address.ContactId
                              into ContactAddressGroup
                              from address in ContactAddressGroup.DefaultIfEmpty()
                              select new FullContact
                              {
                                  FirstName = contact.FirstName,
                                  LastName = contact.LastName,
                                  ZipCode = address.ZipCode,
                                  City = address.City,
                              };
            return query.ToList();
		}
	}
}