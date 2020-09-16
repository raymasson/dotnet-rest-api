using System.Collections.Generic;
using System.Linq;
using Api.Controllers;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Api.Tests
{
    public class ContactsControllerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepoWrapper;
        private readonly Mock<IContactRepository> _mockRepo;
        private readonly Mock<ILoggerManager> _mockLogger;
        private readonly ContactsController _controller;
        private readonly List<Contact> _contacts;

        public ContactsControllerTests()
        {
            // Test contacts
            _contacts = new List<Contact>()
            {
                new Contact() { Id = 1, FirstName = "Michel", LastName="DURAND", Age = 53, Gender= "male" }
            };

            // Mock the repository
            _mockRepoWrapper = new Mock<IRepositoryWrapper>();
            _mockRepo = new Mock<IContactRepository>();
            _mockRepo.Setup(x => x.GetContacts(It.IsAny<ContactParameters>())).Returns(
                PagedList<Contact>.ToPagedList(
                    _contacts.AsQueryable(),
				    1,
				    2
                )
            );
            _mockRepoWrapper.Setup(x => x.Contact).Returns(_mockRepo.Object);

            // Mock the logger
            _mockLogger = new Mock<ILoggerManager>();
            
            // Instanciate the ContactsController
            _controller = new ContactsController(_mockLogger.Object, _mockRepoWrapper.Object);

            // Mock the HTTP response
            var headerDictionary = new HeaderDictionary();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);
            _controller.ControllerContext = new ControllerContext() 
            {
                HttpContext = httpContext.Object
            };
        }

        [Fact]
        public void GetContacts_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetContacts(new ContactParameters());

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetContacts_WhenCalled_WithWrongAgeRange_ReturnsBadRequestResult()
        {
            // Act
            var badResult = _controller.GetContacts(new ContactParameters(){
                 MinAge = 10,
                 MaxAge = 5
            });

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResult.Result);
        }

        [Fact]
        public void GetContacts_WhenCalled_ReturnsAllItems_And_Pagination()
        {
            // Arrange
            var metadata = new
			{
				TotalCount = 1,
				PageSize = 2,
				CurrentPage = 1,
				TotalPages = 1,
				HasNext = false,
				HasPrevious = false
			};

            // Act
            var okResult = _controller.GetContacts(new ContactParameters()).Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<PagedList<Contact>>(okResult.Value);
            Assert.Single(items);
            Assert.Equal(JsonConvert.SerializeObject(metadata), _controller.HttpContext.Response.Headers["X-Pagination"]);
        }
    }
}
