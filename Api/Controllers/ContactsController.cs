using Contracts;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
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
		public ActionResult<PagedList<Contact>> GetContacts([FromQuery] ContactParameters contactParameters)
		{
			if (!contactParameters.ValidAgeRange)
			{
				_logger.LogWarn("Wrong parameter: Age");
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

			_logger.LogInfo($"Returned {contacts.TotalCount} contacts from database.");

			return Ok(contacts);
		}
    }
}