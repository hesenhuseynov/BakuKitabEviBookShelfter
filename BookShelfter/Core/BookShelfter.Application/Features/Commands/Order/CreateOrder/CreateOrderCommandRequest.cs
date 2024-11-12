using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Order;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Order.CreateOrder
{
    public  class CreateOrderCommandRequest:IRequest<CreateOrderCommandResponse>
    {
        public int   CustomerId { get; set; }
        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; } 
        
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
