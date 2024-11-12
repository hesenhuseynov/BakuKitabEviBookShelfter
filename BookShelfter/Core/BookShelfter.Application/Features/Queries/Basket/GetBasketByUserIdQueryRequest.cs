using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Basket
{
    public  class GetBasketByUserIdQueryRequest:IRequest<GetBasketByUserIdQueryResponse>
    {
        public string UserId { get; set; }
    }
}
