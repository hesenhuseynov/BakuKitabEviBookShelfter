using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.SearchBooksQuery
{
    public  class SearchBooksQueryHandler:IRequestHandler<SearchBooksQueryRequest,SearchBooksQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;

        public SearchBooksQueryHandler(IBookReadRepository bookReadRepository)
        {
            _bookReadRepository = bookReadRepository;
        }

        public  async Task<SearchBooksQueryResponse> Handle(SearchBooksQueryRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookReadRepository.SearchBookAsync(request.Keyword);

            var bookDtos = books.Select(b => new BookDto
            {
                BookId = b.Id,
                BookName = b.BookName,
                AuthorName = b.AuthorName,
                Stock = b.Stock,
                Price = b.Price,
                Description = b.Description,
                CategoryId = b.CategoryId,
                ImageUrls = b.BookImagesFile.Select(img => img.ImageUrl).ToList(),
                IsFeatured = b.IsFeatured
            }).ToList();

            int totalCount=bookDtos.Count;

            return new()
            {
                Books = bookDtos,
                TotalCount = totalCount
            };

        }
    }
}
