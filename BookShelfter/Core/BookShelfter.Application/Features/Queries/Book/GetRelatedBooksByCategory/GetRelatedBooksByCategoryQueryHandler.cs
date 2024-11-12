using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetRelatedBooksByCategory
{
    public  class GetRelatedBooksByCategoryQueryHandler:IRequestHandler<GetRelatedBooksByCategoryQueryRequest,GetRelatedBooksByCategoryQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;

        public GetRelatedBooksByCategoryQueryHandler(IBookReadRepository bookReadRepository)
        {
            _bookReadRepository = bookReadRepository;
        }

        public  async Task<GetRelatedBooksByCategoryQueryResponse> Handle(GetRelatedBooksByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var getRelatedBooks = await _bookReadRepository.GetRelatedBooksByCategoryAsync(request.CategoryId);


            // Kitabın kendisini listeden çıkarıyoruz
            getRelatedBooks = getRelatedBooks.Where(book => book.Id != request.BookId).ToList();


            if (!getRelatedBooks.Any())
            {
                return new()
                {
                    Message = "Not have any  books  in this category related"
                };
            }

            return new()
            {
                Message = "Success ",
                Success = true,
                BookDtos=  getRelatedBooks.Select(c=> new BookDto()
                {
                    BookId = c.Id,  
                    AuthorName = c.AuthorName,
                    BookName = c.BookName,
                    Description = c.Description,
                    Stock = c.Stock,
                    Price = c.Price,
                    ImageUrls = c.BookImagesFile?.Select(img => img.ImageUrl).ToList()

                })
            };

        }
    }
}
