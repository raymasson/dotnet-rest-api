using System;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private ILoggerManager _logger;
		private IRepositoryWrapper _repository;

        public ContactsController(ILoggerManager logger, IRepositoryWrapper repository)
		{
			_logger = logger;
			_repository = repository;
		}

        [HttpGet]
		public IActionResult GetContacts([FromQuery] ContactParameters contactParameters)
		{
			if (!contactParameters.ValidAgeRange)
			{
				return BadRequest("Max age cannot be less than min age");
			}

			var contacts = _repository.Contact.GetContacts(contactParameters);

			var metadata = new
			{
				contacts.TotalCount,
				contacts.PageSize,
				contacts.CurrentPage,
				contacts.TotalPages,
				contacts.HasNext,
				contacts.HasPrevious
			};

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

			_logger.LogInfo($"Returned {contacts.TotalCount} owners from database.");

			return Ok(contacts);
		}
    }
}