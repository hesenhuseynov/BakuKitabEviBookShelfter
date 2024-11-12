using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Order;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Order.UpdateOrder
{
    public  class UpdateOrderCommandRequest:IRequest<UpdateOrderCommandResponse>
    {
        public int  OrderId { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}

