using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Basket.RemoveItemFromBasket
{
    public  class RemoveItemFromBasketCommandRequest:IRequest<RemoveItemFromBasketCommandResponse>
    {
        public string UserId { get; set; }

        public int bookId { get; set; }

        

    }
}
