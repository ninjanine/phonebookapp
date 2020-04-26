using System;
using Xunit;
using Moq;
using PhonebookApi.Models;
using PhonebookApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhonebookApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace phonebookapi.tests
{
    public class PhoneBookControllerTest
    {
        Mock<IPhoneBookRepository> _mockRepo;
        List<PhoneBook> _phoneBooks;
        public PhoneBookControllerTest()
        {
            _mockRepo = new Mock<IPhoneBookRepository>();
            _phoneBooks = new List<PhoneBook>();
        }

        [Fact]
        public async Task Get_ReturnsHttpNotFound_WhenNoData()
        {
            // Arrange
            
            _mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync((IEnumerable<PhoneBook>)null);
            var controller = new PhoneBookController(_mockRepo.Object);

            // Act
            var result = await controller.Get();
            
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsHttpOk_WhenDataIsPresent()
        {
            // Arrange
            
            
            _mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync((IEnumerable<PhoneBook>)_phoneBooks);
            var controller = new PhoneBookController(_mockRepo.Object);

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
            
            
            var controller = new PhoneBookController(_mockRepo.Object);

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
            
            _mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBook);

            var controller = new PhoneBookController(_mockRepo.Object);

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
            

            var controller = new PhoneBookController(_mockRepo.Object);

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
            
            _mockRepo.Setup(repo => repo.Create(mockPhoneBookEntry));

            var controller = new PhoneBookController(_mockRepo.Object);

            // Act
            var result = controller.Create(mockPhoneBookEntry);

            // Assert
            _mockRepo.Verify(repo => repo.Create(mockPhoneBookEntry), Times.Once);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void Update_ReturnsHttpNotFound_WhenEmptyIdSupplied()
        {
            // Arrange
            
            var controller = new PhoneBookController(_mockRepo.Object);

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
            
            _mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(_mockRepo.Object);
            

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
            
            _mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(_mockRepo.Object);

            // Act
            var result = controller.Update(id, mockPhoneBookEntry);

            // Assert
            _mockRepo.Verify(repo => repo.Update(id, mockPhoneBookEntry), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnsHttpNotFound_WhenEmptyIdSupplied()
        {
            // Arrange
            
            var controller = new PhoneBookController(_mockRepo.Object);

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
            
            _mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(_mockRepo.Object);


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
            
            _mockRepo.Setup(repo => repo.Get(id))
                .ReturnsAsync(mockPhoneBookEntry);

            var controller = new PhoneBookController(_mockRepo.Object);

            // Act
            var result = controller.Delete(id);

            // Assert
            _mockRepo.Verify(repo => repo.Delete(id), Times.Once);
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task Search_ReturnsHttpOk_WhenContentIsFound()
        {
            // Arrange
            string searchquery = "contact one";
            
            _mockRepo.Setup(repo => repo.Search(searchquery))
                .ReturnsAsync(_phoneBooks);

            var controller = new PhoneBookController(_mockRepo.Object);

            // Act
            var result = await controller.Search(searchquery);

            // Assert
            _mockRepo.Verify(repo => repo.Search(searchquery), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}
