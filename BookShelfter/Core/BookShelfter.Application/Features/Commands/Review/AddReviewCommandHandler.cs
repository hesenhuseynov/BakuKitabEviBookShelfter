using BookShelfter.Application.DTOs.Review;
using BookShelfter.Application.Repositories.Review;
using BookShelfter.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Review
{
    public  class AddReviewCommandHandler:IRequestHandler<AddReviewCommandRequest,AddReviewCommandResponse>
    {
        private readonly IReviewWriteRepository _reviewWriteRepository;
        private readonly IReviewReadRepository _reviewReadRepository;   

        public AddReviewCommandHandler(IReviewWriteRepository reviewWriteRepository, IReviewReadRepository reviewReadRepository)
        {
            _reviewWriteRepository = reviewWriteRepository;
            _reviewReadRepository = reviewReadRepository;
        }

        public async  Task<AddReviewCommandResponse> Handle(AddReviewCommandRequest request, CancellationToken cancellationToken)
        {


            var existingReview =
                await _reviewReadRepository.GetReviewByBookAndUserAsync(request.BookId, request.UserId);

            if (existingReview!=null)
            {
                return new()
                {
                    Success = false,
                    Message = $"{request.UserName} bu kitab üçün artıq bir şərh bildirmisiniz."
                };

            }


            var review = new Reviews
            {
                BookID = request.BookId,
                UserID = request.UserId,
                UserName = request.UserName,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewDate = DateTime.UtcNow
            };

            await _reviewWriteRepository.AddAsync(review);
            await _reviewWriteRepository.SaveAsync();

            return new AddReviewCommandResponse
            {
                ReviewId = review.Id,
                Success = true,
                Message = "Comment added successfully "
            };


        }
    }
}
