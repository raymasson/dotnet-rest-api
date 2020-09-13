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

        public RepositoryWrapper(RepositoryContext repositoryContext,
            ISortHelper<Contact> contactSortHelper)
        {
            _repoContext = repositoryContext;
            _contactSortHelper = contactSortHelper;
        }
    }
}