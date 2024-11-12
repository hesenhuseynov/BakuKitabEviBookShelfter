using BookShelfter.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Order.GetById
{
    public  class GetOrderByIdQueryResponse
    {
        public int  OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
