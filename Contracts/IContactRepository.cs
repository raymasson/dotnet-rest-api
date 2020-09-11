using Entities.Helpers;
using Entities.Models;
using System;

namespace Contracts
{
	public interface IContactRepository : IRepositoryBase<Contact>
	{
		PagedList<Contact> GetContacts(ContactParameters contactParameters);
	}
}