using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Basket.ClearBasket
{
    public  class ClearBasketCommandRequest:IRequest<ClearBasketCommandResponse>
    {
        public string UserId { get; set; }
    }
}
