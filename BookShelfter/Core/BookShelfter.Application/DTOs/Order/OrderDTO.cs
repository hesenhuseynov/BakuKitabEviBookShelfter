using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities.Enums;

namespace BookShelfter.Application.DTOs.Order
{
    public  class OrderDTO
    {
        public int   Id { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status    { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string OrderCode { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
