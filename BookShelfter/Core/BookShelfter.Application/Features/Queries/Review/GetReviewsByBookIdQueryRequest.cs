using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Review
{
    public class GetReviewsByBookIdQueryRequest:IRequest<GetReviewsByBookIdQueryResponse>
    {
        public int BookId { get; set; }
        
    }
}