using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.GetNewArrivalsBooks
{
    public  class GetNewArrivalsBooksQueryHandler:IRequestHandler<GetNewArrivalsBooksQueryRequest,GetNewArrivalsBooksQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly ICacheService _cacheService;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public GetNewArrivalsBooksQueryHandler(IBookReadRepository bookReadRepository, ICacheService cacheService)
        {
            _bookReadRepository = bookReadRepository;
            _cacheService = cacheService;
        }

        public  async Task<GetNewArrivalsBooksQueryResponse> Handle(GetNewArrivalsBooksQueryRequest request, CancellationToken cancellationToken)
        {
            //string cacheKey = "new_arrivals_books_cache";
            //if (_cacheService.TryGetValue(cacheKey, out List<BookDto> cachedBooks))
            //{

            //    return new()
            //    {
            //        Books = cachedBooks,
            //        TotalBookCount = cachedBooks.Count
            //    };

                

            //}



            

            var books = await _bookReadRepository.GetNewArrivalsAsync();
            var booksDtos = books.Select(book => new BookDto
            {
                BookId = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                Price = book.Price,
                ImageUrls = book.BookImagesFile.Select(img => img.ImageUrl).ToList(),
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
