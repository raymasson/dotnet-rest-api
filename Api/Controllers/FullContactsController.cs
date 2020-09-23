using System.Collections.Generic;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// The ContactsController class.
    /// Contains all actions for performing CRUD on contacts.
    /// </summary>
    [Route("api/[controller]")]
    public class FullContactsController : ControllerBase
    {
		private IRepositoryWrapper _repository;

        public FullContactsController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

        [HttpGet]
		public ActionResult<List<FullContact>> GetFullContacts()
		{
            var fullContacts = _repository.Address.GetFullContacts();

            return Ok(fullContacts);
		}
    }
}