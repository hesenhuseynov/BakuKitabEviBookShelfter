using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Review;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Review
{
    public class GetReviewsByBookIdQueryHandler : IRequestHandler<GetReviewsByBookIdQueryRequest, GetReviewsByBookIdQueryResponse>
    {
        private readonly IReviewReadRepository _readRepository;

        public GetReviewsByBookIdQueryHandler(IReviewReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetReviewsByBookIdQueryResponse> Handle(GetReviewsByBookIdQueryRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _readRepository.GetReviewsByBookIdAsync(request.BookId);

            if (reviews == null || !reviews.Any())
            {
                return new GetReviewsByBookIdQueryResponse
                {
                    Success = true,
                    Message = "Bu kitaba ait heçbir  şərh yoxdur.",
                    Reviews = new List<Reviews>()
                };
            }

            return new GetReviewsByBookIdQueryResponse
            {
                Success = true,
                Message = "Proses uğurla həyata keçirildi.",
                Reviews = reviews
            };




        }


    }
}