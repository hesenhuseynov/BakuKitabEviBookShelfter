using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Features.Queries.Book.GetNewArrivalsBooks;
using BookShelfter.Application.Repositories.Book;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Test.Features.Queries.Books
{
    public  class GetNewArrivalsBookQueryHandlerTests
    {



        [Fact]
        public async Task GetNewArrivalsBooks_Should_Return_CachedBooks_When_CacheExists()
        {
            // Arrange
            var mockCacheService = new Mock<ICacheService>();
            var mockBookRepository = new Mock<IBookReadRepository>();
            var handler = new GetNewArrivalsBooksQueryHandler(mockBookRepository.Object, mockCacheService.Object);

            var cachedBooks = new List<BookDto>
            {
                new BookDto { BookId = 1, BookName = "Book 1", AuthorName = "Author 1" },
                new BookDto { BookId = 2, BookName = "Book 2", AuthorName = "Author 2" }
            };

            mockCacheService.Setup(x => x.TryGetValue(It.IsAny<string>(), out cachedBooks)).Returns(true);

            // Act
            var result = await handler.Handle(new GetNewArrivalsBooksQueryRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cachedBooks.Count, result.Books.Count);
            Assert.Equal(cachedBooks, result.Books);
        }


    }
}
