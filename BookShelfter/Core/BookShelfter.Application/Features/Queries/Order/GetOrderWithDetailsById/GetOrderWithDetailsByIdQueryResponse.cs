using BookShelfter.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Order.GetOrderWithDetailsById
{
    public  class GetOrderWithDetailsByIdQueryResponse
    {
        public bool  Success { get; set; }
        public string Message { get; set; }

        public int OrderId { get; set; }
        public string  OrderNumber { get; set; }
        public string PaymentMethod { get; set; }

        public string TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int CustomerId { get; set; }
        public string  CustomerPhoneNumber { get; set; }

        public List<OrderDetailsDTO> OrderDetails { get; set; }

    }
}
