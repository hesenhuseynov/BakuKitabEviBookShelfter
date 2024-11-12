using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Order;

namespace BookShelfter.Application.Features.Queries.Order.GetAll
{
    public  class GetAllOrdersQueryResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<OrderDTO>Orders { get; set; }
    }
}
