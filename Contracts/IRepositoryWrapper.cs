using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IContactRepository Contact { get; }
        IAddressRepository Address { get; }
	}
}