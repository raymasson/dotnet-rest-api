using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IContactRepository _contact;

        private IAddressRepository _address;
        private ISortHelper<Contact> _contactSortHelper;

        public IContactRepository Contact
        {
            get
            {
                if (_contact == null)
                {
                    _contact = new ContactRepository(_repoContext, _contactSortHelper);
                }

                return _contact;
            }
        }

        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_repoContext);
                }

                return _address;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext,
            ISortHelper<Contact> contactSortHelper)
        {
            _repoContext = repositoryContext;
            _contactSortHelper = contactSortHelper;
        }
    }
}