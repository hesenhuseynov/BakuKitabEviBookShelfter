using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Basket.AddItem
{
    public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResponse>
    {
        public string UserID { get; set; }

        public int bookId { get; set; }
        public int Quantity { get; set; }


    }
}
