using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Application.Repositories.Review;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetBookDetails
{
    public  class GetBookDetailsQueryHandler:IRequestHandler<GetBookDetailsQueryRequest,GetBookDetailsQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly IReviewReadRepository _reviewReadRepository;

        public GetBookDetailsQueryHandler(IReviewReadRepository reviewReadRepository, IBookReadRepository bookReadRepository)
        {
            _reviewReadRepository = reviewReadRepository;
            _bookReadRepository = bookReadRepository;
        }


        public async  Task<GetBookDetailsQueryResponse> Handle(GetBookDetailsQueryRequest request, CancellationToken cancellationToken)
        {
    var book = await _bookReadRepository.GetBookByIdWithImagesAsync(request.BookId);
            if (book == null)
            {
                return new()
                {
                    Message = " bu Id ya aid kitab yoxdur ",
                    Success = false

                };
            }

            var averageRating = await _reviewReadRepository.GetAverageRatingForBookAsync(request.BookId);
            var totalReviews = await _reviewReadRepository.GetTotalReviewsForBookAsync(request.BookId);


            var bookDetailDto = new BookDetailDto
            {
                BookId = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                Stock = book.Stock,
                Price = book.Price,
                Description = book.Description,
                CategoryId = book.CategoryId,
                ImageUrls = book.BookImagesFile.Select(img=>img.ImageUrl).ToList(),
                AverageRating = averageRating,
                TotalReviews = totalReviews
            };


            return new()
            {
                Book = bookDetailDto,
                Message="operation success",
                 Success=true
            };

        }

    }
}
