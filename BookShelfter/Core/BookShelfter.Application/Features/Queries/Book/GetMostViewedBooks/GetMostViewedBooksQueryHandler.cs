using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.GetMostViewedBooks
{
    public  class GetMostViewedBooksQueryHandler:IRequestHandler<GetMostViewedBooksQueryRequest,GetMostViewedBooksQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly ICacheService _cacheService;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30); 

        public GetMostViewedBooksQueryHandler(ICacheService cacheService, IBookReadRepository bookReadRepository)
        {
            _cacheService = cacheService;
            _bookReadRepository = bookReadRepository;
        }

        public async  Task<GetMostViewedBooksQueryResponse> Handle(GetMostViewedBooksQueryRequest request, CancellationToken cancellationToken)
        {
            //string cacheKey = "most_viewed_books_cache";

            //if (_cacheService.TryGetValue(cacheKey, out List<BookDto> cachedBooks))
            //{
            //    return new GetMostViewedBooksQueryResponse
            //    {
            //        Books = cachedBooks,
            //        TotalBookCount = cachedBooks.Count
            //    };
            //}

            var books = await _bookReadRepository.GetMostViewedBookAsync();

            var booksDtos = books.Select(book => new BookDto
            {
                BookId = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                Price = book.Price,
                ImageUrls = book.BookImagesFile.Select(img => img.ImageUrl).ToList(),
            }).ToList();

            //_cacheService.Set(cacheKey, booksDtos, _cacheDuration);

            return new GetMostViewedBooksQueryResponse
            {
                Books = booksDtos,
                TotalBookCount = booksDtos.Count
            };

        }
    }
}
