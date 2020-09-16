using System.Collections.Generic;
using System.Linq;
using Entities.Helpers;
using Entities.Models;
using Xunit;

namespace Entities.Tests.Helpers
{
    public class SortHelperTests
    {
        private readonly List<Contact> _contacts;

        private readonly ISortHelper<Contact> _sortHelper;

        public SortHelperTests()
        {
            // Test contacts
            _contacts = new List<Contact>()
            {
                new Contact() { Id = 1, LastName = "THOMSON" },
                new Contact() { Id = 2, LastName = "BERKIN" },
                new Contact() { Id = 3, LastName = "URSUL" }
            };

            _sortHelper = new SortHelper<Contact>();
        }

        [Fact]
        public void ApplySort_WhenCalled_ReturnsSortedList()
        {
            // Act
            var lastNameAsc = _sortHelper.ApplySort(_contacts.AsQueryable(), "lastName asc");
            var lastNameDesc = _sortHelper.ApplySort(_contacts.AsQueryable(), "lastName desc");

            // Assert
            Assert.IsType<EnumerableQuery<Contact>>(lastNameAsc);
            Assert.IsType<EnumerableQuery<Contact>>(lastNameDesc);
            Assert.Equal(_contacts[1].LastName, lastNameAsc.ToList()[0].LastName);
            Assert.Equal(_contacts[0].LastName, lastNameAsc.ToList()[1].LastName);
            Assert.Equal(_contacts[2].LastName, lastNameAsc.ToList()[2].LastName);
            Assert.Equal(_contacts[2].LastName, lastNameDesc.ToList()[0].LastName);
            Assert.Equal(_contacts[0].LastName, lastNameDesc.ToList()[1].LastName);
            Assert.Equal(_contacts[1].LastName, lastNameDesc.ToList()[2].LastName);
        }
    }
}