using System.Collections.Generic;
using System.Linq;
using Entities.Helpers;
using Entities.Models;
using Xunit;

namespace Entities.Tests.Helpers
{
    public class PagedListTests
    {
        private readonly List<Contact> _contacts;

        public PagedListTests()
        {
            // Test contacts
            _contacts = new List<Contact>()
            {
                new Contact() { Id = 1 },
                new Contact() { Id = 2 },
                new Contact() { Id = 3 },
                new Contact() { Id = 4 },
                new Contact() { Id = 5 }
            };
        }

        [Fact]
        public void ToPagedList_WhenCalled_ReturnsPagedList()
        {
            // Act
            var firstPage = PagedList<Contact>.ToPagedList(_contacts.AsQueryable(), 1, 2);
            var secondPage = PagedList<Contact>.ToPagedList(_contacts.AsQueryable(), 2, 2);
            var thirdPage = PagedList<Contact>.ToPagedList(_contacts.AsQueryable(), 3, 2);

            // Assert
            Assert.IsType<PagedList<Contact>>(firstPage);
            Assert.IsType<PagedList<Contact>>(secondPage);
            Assert.IsType<PagedList<Contact>>(thirdPage);
            Assert.True(firstPage.Count == 2);
            Assert.True(secondPage.Count == 2);
            Assert.True(thirdPage.Count == 1);
            Assert.Equal(_contacts.GetRange(0, 2), firstPage);
            Assert.Equal(_contacts.GetRange(2, 2), secondPage);
            Assert.Equal(_contacts.GetRange(4, 1), thirdPage);
            Assert.Equal(5, firstPage.TotalCount);
            Assert.Equal(3, firstPage.TotalPages);
            Assert.Equal(2, firstPage.PageSize);
            Assert.Equal(1, firstPage.CurrentPage);
            Assert.False(firstPage.HasPrevious);
            Assert.True(firstPage.HasNext);
            Assert.Equal(2, secondPage.CurrentPage);
            Assert.True(secondPage.HasPrevious);
            Assert.True(secondPage.HasNext);
            Assert.Equal(3, thirdPage.CurrentPage);
            Assert.True(thirdPage.HasPrevious);
            Assert.False(thirdPage.HasNext);
        }
    }
}