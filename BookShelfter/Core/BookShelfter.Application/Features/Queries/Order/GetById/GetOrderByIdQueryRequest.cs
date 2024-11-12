using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Order.GetById
{
    public  class GetOrderByIdQueryRequest:IRequest<GetOrderByIdQueryResponse>
    {
        public int  OrderId { get; set; }

    }
}
