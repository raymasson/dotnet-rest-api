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

		public List<FullContact> GetFullContacts(ContactParameters contactParameters)
		{              
            // INNER JOIN lambda
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

            // LEFT JOIN lambda
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

            // LEFT JOIN Query syntax
            /*var query = from contact in this.RepositoryContext.Contacts
                              join address in this.RepositoryContext.Addresses
                              on contact.Id equals address.ContactId
                              into ContactAddressGroup
                              from address in ContactAddressGroup.DefaultIfEmpty()
                              select new FullContact
                              {
                                  FirstName = contact.FirstName,
                                  LastName = contact.LastName,
                                  Gender = contact.Gender,
                                  ZipCode = address.ZipCode,
                                  City = address.City,
                              };*/

            List<FullContact> fullContacts = new List<FullContact>{};

            using (var context = this.RepositoryContext) {
                var query = from contact in context.Contacts 
                        join child in context.Children on contact.Id equals child.ContactId
                        into ChildGroup
                        from c in ChildGroup.DefaultIfEmpty()
                        join address in context.Addresses on contact.Id equals address.ContactId
                        into AddressGroup
                        from a in AddressGroup.DefaultIfEmpty()
                        select new { 
                            Contact = contact,
                            Address = a, 
                            Child = c
                        };  
                // Filters
                if (!string.IsNullOrEmpty(contactParameters.Gender)) {
                    query = query.Where(q => q.Contact.Gender == contactParameters.Gender);
                }

                // Group by contact
                var grouping = query.ToLookup(q => q.Contact.Id);   
                Console.WriteLine("Items Count: {0}", grouping.Count);  

                foreach (var item in grouping)
                {
                    var fullContact = new FullContact() {
                        FirstName = item.First().Contact.FirstName,
                        LastName = item.First().Contact.LastName,
                        Gender = item.First().Contact.Gender,
                        ZipCode = item.First().Address == null ? 0 : item.First().Address.ZipCode,
                        City = item.First().Address?.City,
                        Children = new List<Child>{}
                    };
                    
                    foreach (var i in item) {
                        if (i.Child != null) {
                            fullContact.Children.Add(
                                new Child(){
                                    Name = i.Child.Name
                                }
                            );
                        }
                    }

                    fullContacts.Add(fullContact);
                }
            }
                  
            return fullContacts;
		}
	}
}