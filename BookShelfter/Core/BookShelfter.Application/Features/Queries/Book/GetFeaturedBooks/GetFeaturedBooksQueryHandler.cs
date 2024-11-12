using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetFeaturedBooks
{
    public  class GetFeaturedBooksQueryHandler:IRequestHandler<GetFeaturedBooksQueryRequest,GetFeaturedBooksQueryResponse>
    {
        public readonly IBookReadRepository _bookReadRepository;
        public readonly ICacheService _cacheService;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public GetFeaturedBooksQueryHandler(IBookReadRepository bookReadRepository, ICacheService cacheService)
        {
            _bookReadRepository = bookReadRepository;
            _cacheService = cacheService;
        }

        public  async Task<GetFeaturedBooksQueryResponse> Handle(GetFeaturedBooksQueryRequest request, CancellationToken cancellationToken)
        {

            //string cacheKey = "featured_books_cache";
            //if (_cacheService.TryGetValue(cacheKey, out List<BookDto> cachedBooks))
            //{
            //    return new()
            //    {
            //        Books = cachedBooks,
            //        TotalBookCount = cachedBooks.Count
            //    };
            //}

            var books = await _bookReadRepository.GetFeaturedBooksAsync();



            if (books == null || !books.Any())
            {

                return new()
                {
                    Books = new List<BookDto>(),
                    TotalBookCount = 0
                };



            }

            var booksDtos = books.Select(book => new BookDto()
            {
                BookId = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                Price = book.Price,
                ImageUrls = book.BookImagesFile.Select(img => img.ImageUrl).ToList()
            }).ToList();


            //_cacheService.Set(cacheKey, booksDtos, _cacheDuration);


            return new()
            {
                Books = booksDtos,
                TotalBookCount = booksDtos.Count
            };
        }
    }
}
