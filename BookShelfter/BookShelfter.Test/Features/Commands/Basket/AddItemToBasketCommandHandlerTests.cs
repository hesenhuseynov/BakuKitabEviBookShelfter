using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Commands.Basket.AddItem;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Domain.Entities;
using BookShelfter.Test.Helpers;
using Moq;
using Serilog;

namespace BookShelfter.Test.Features.Commands.Basket
{
    public class AddItemToBasketCommandHandlerTests
    {
        private readonly Mock<IBasketReadRepository> _mockBasketReadRepository;
        private readonly Mock<IBasketWriteRepository> _mockBasketWriteRepository;
        private readonly Mock<IBookReadRepository> _mockBookReadRepository;
        private readonly AddItemToBasketCommandHandler _handler;



        public AddItemToBasketCommandHandlerTests()
        {


  


            _mockBasketReadRepository = new Mock<IBasketReadRepository>();
            _mockBasketWriteRepository = new Mock<IBasketWriteRepository>();
            _mockBookReadRepository = new Mock<IBookReadRepository>();
            _handler = new AddItemToBasketCommandHandler
            (
                _mockBasketReadRepository.Object,
                _mockBasketWriteRepository.Object,
                _mockBookReadRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_Add_New_Item_To_Basket_When_Not_Exists()
        {
            // Arrange
            var command = TestHelpers.CreateTestAddItemToBasketCommandRequest();
            var book = TestHelpers.CreateTestBook();
            var logger = new  Mock<ILogger>();


            Assert.NotNull(book);

            Assert.NotNull(command);

         var  booksHaveOrNot   =    _mockBookReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(book);
            _mockBasketReadRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync((Domain.Entities.Basket?)null);
            _mockBasketWriteRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Basket>())).Returns(Task.FromResult(true));
            _mockBasketWriteRepository.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
      
            Assert.True(result.Success);
            Assert.Equal("Book added to basket successfully", result.Message);
             
            _mockBasketWriteRepository.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Basket>()), Times.Once);
            _mockBasketWriteRepository.Verify(x => x.SaveAsync(), Times.Once);
            


        }

        [Fact]
        public async Task Handle_Should_Update_Existing_Item_Quantity_When_Already_In_Basket()
        {
            // Arrange
            var command = TestHelpers.CreateTestAddItemToBasketCommandRequest();
            var book = TestHelpers.CreateTestBook();
            var basket = TestHelpers.CreateTestBasket();

                _mockBasketReadRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(basket);
                _mockBookReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(book);
                _mockBasketWriteRepository.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Book added to basket successfully.", result.Message);
            var updatedBasketItem = basket.BasketItems.FirstOrDefault(i => i.BookId == command.bookId);
            Assert.NotNull(updatedBasketItem);
            Assert.Equal(2, updatedBasketItem.Quantity);
            _mockBasketWriteRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Book_Does_Not_Exist()
        {
            // Arrange
            var command = TestHelpers.CreateTestAddItemToBasketCommandRequest();

            _mockBookReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((Book)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid Book ID", result.Message);
            _mockBasketWriteRepository.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Basket>()), Times.Never);
            _mockBasketWriteRepository.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Basket_Cannot_Be_Added()
        {
            // Arrange
            var command = TestHelpers.CreateTestAddItemToBasketCommandRequest();
            var book = TestHelpers.CreateTestBook();

            _mockBookReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(book);
            _mockBasketReadRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync((Domain.Entities.Basket)null);
            _mockBasketWriteRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Basket>())).Returns(Task.FromResult(false));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Failed to add basket", result.Message);
            _mockBasketWriteRepository.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
