using System;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using PhonebookApi.Models;
using PhonebookApi.Repository;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhonebookApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace phonebookapi.tests
{
    public class PhoneBookControllerTest
    {
        public PhoneBookControllerTest()
        {
        }

        [Fact]
        public async Task Get_ReturnsHttpNotFound_WhenNoData()
        {
            // Arrange
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync((IEnumerable<PhoneBook>)null);
            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = await controller.Get();
            
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsHttpOk_WhenDataIsPresent()
        {
            // Arrange
            var phoneBooks = new List<PhoneBook>();
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync((IEnumerable<PhoneBook>)phoneBooks);
            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenNoIdSupplied()
        {
            // Arrange
            Guid id = Guid.Empty;
            PhoneBook mockPhoneBook = new PhoneBook();
            var mockRepo = new Mock<IPhoneBookRepository>();
            
            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsHttpOk_WhenIdSupplied()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            PhoneBook mockPhoneBook = new PhoneBook();
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBook);

            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenNoPhoneBookEntry()
        {
            // Arrange
            PhoneBook mockPhoneBookEntry = null;
            var mockRepo = new Mock<IPhoneBookRepository>();

            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Create(mockPhoneBookEntry);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtRoute_WhenNewPhoneBookEntryAdded()
        {
            // Arrange
            PhoneBook mockPhoneBookEntry = new PhoneBook();
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Create(mockPhoneBookEntry));

            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Create(mockPhoneBookEntry);

            // Assert
            mockRepo.Verify(repo => repo.Create(mockPhoneBookEntry), Times.Once);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void Update_ReturnsHttpNotFound_WhenEmptyIdSupplied()
        {
            // Arrange
            var mockRepo = new Mock<IPhoneBookRepository>();
            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Update(Guid.Empty, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsHttpNotFound_WhenNoContentLoaded()
        {
            // Arrange
            Guid id = new Guid();
            PhoneBook mockPhoneBookEntry = null;
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(mockRepo.Object);
            

            // Act
            var result = controller.Update(Guid.Empty, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsHttpOk_WhenContentIsUpdated()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            PhoneBook mockPhoneBookEntry = new PhoneBook();
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Update(id, mockPhoneBookEntry);

            // Assert
            mockRepo.Verify(repo => repo.Update(id, mockPhoneBookEntry), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnsHttpNotFound_WhenEmptyIdSupplied()
        {
            // Arrange
            var mockRepo = new Mock<IPhoneBookRepository>();
            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Delete(Guid.Empty);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsHttpNotFound_WhenNoContentLoaded()
        {
            // Arrange
            Guid id = new Guid();
            PhoneBook mockPhoneBookEntry = null;
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(mockRepo.Object);


            // Act
            var result = controller.Delete(Guid.Empty);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsHttpOk_WhenContentIsDeleted()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            PhoneBook mockPhoneBookEntry = new PhoneBook();
            var mockRepo = new Mock<IPhoneBookRepository>();
            mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(mockRepo.Object);

            // Act
            var result = controller.Delete(id);

            // Assert
            mockRepo.Verify(repo => repo.Delete(id), Times.Once);
            Assert.IsType<OkResult>(result);
        }
        
    }
}
