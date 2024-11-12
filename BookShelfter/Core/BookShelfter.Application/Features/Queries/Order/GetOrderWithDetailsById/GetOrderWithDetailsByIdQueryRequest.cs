using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Order.GetOrderWithDetailsById
{
    public  class GetOrderWithDetailsByIdQueryRequest:IRequest<GetOrderWithDetailsByIdQueryResponse>
    {
        public int OrderId { get; set; }

    }
}
