using Entities.Models;
using Contracts;
using Entities.Helpers;
using Entities;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Tests
{
    public class ContactRepositoryTests
    {
        private readonly IContactRepository _repo;

        private readonly ISortHelper<Contact> _sortHelper;

        public ContactRepositoryTests()
        {
            // Sort helper
            _sortHelper = new SortHelper<Contact>();

            // Test DB context for contacts
            var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "ContactsDatabase")
            .Options;
            var context = new RepositoryContext(options);
            context.Contacts.Add(new Contact() { Id = 1, Age = 10, Gender= "male" });
            context.Contacts.Add(new Contact() { Id = 2, Age = 20, Gender= "male" });
            context.Contacts.Add(new Contact() { Id = 3, Age = 30, Gender= "male" });
            context.Contacts.Add(new Contact() { Id = 4, Age = 40, Gender= "female" });
            context.Contacts.Add(new Contact() { Id = 5, Age = 50, Gender= "female" });
            context.Contacts.Add(new Contact() { Id = 6, Age = 60, Gender= "female" });
            context.SaveChanges();

            // Repo
            _repo = new ContactRepository(context, _sortHelper);
        }

        [Fact]
        public void GetContacts_WhenCalled_ReturnsFilteredList()
        {
            // Act
            var filteredMaleList = _repo.GetContacts(new ContactParameters(){
                MinAge = 20,
                MaxAge = 50,
                Gender = "male"
            });
            var filteredFemaleList = _repo.GetContacts(new ContactParameters(){
                MinAge = 20,
                MaxAge = 50,
                Gender = "female"
            });
        
            // Assert
            Assert.IsType<PagedList<Contact>>(filteredMaleList);
            Assert.IsType<PagedList<Contact>>(filteredFemaleList);
            Assert.True(filteredMaleList.Count == 2);
            Assert.True(filteredFemaleList.Count == 2);
            Assert.Equal(2, filteredMaleList[0].Id);
            Assert.Equal(3, filteredMaleList[1].Id);
            Assert.Equal(4, filteredFemaleList[0].Id);
            Assert.Equal(5, filteredFemaleList[1].Id);
        }
    }
}