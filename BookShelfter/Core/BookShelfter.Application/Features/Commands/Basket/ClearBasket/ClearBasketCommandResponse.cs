using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Basket.ClearBasket
{
    public  class ClearBasketCommandResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
