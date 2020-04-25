using System;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using PhonebookApi.Models;
using PhonebookApi.Repository;

namespace phonebookapi.tests
{
    public class RepositoryTests
    {
        private Mock<IOptions<MongoDBSettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public RepositoryTests()
        {
            _mockOptions = new Mock<IOptions<MongoDBSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void MongDBContext_ShouldInitContructor_Successfully()
        {
            var settings = new MongoDBSettings()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoPhoneBookDBContext(_mockOptions.Object);

            //Assert 
            Assert.NotNull(context);
        }

        [Fact]
        public void MongoDBGetCollection_ShouldFail_CollectionNameNotSupplied()
        {

            //Arrange
            var settings = new MongoDBSettings()
            {
                ConnectionString = "mongodb://tes123",
                DatabaseName = "TestDB",
                PhoneBooksCollectionName = ""
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoPhoneBookDBContext(_mockOptions.Object);
            var myCollection = context.GetCollection<PhoneBook>();

            //Assert 
            Assert.Null(myCollection);

        }

        [Fact]
        public void MongoDBGetCollection_ShouldReturn_ValidCollection()
        {
            //Arrange
            var settings = new MongoDBSettings()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB",
                PhoneBooksCollectionName = "123"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new MongoPhoneBookDBContext(_mockOptions.Object);
            var myCollection = context.GetCollection<PhoneBook>();

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
