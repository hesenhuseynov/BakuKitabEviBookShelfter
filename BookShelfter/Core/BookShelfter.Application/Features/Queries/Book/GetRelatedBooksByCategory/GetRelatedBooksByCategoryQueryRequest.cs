using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.GetRelatedBooksByCategory
{
    public  class GetRelatedBooksByCategoryQueryRequest:IRequest<GetRelatedBooksByCategoryQueryResponse>
    {
        public int CategoryId { get; set; }
        public int BookId { get; set; }
    }
}
