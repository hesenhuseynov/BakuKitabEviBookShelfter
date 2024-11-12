using BookShelfter.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Customer
{
    public  class GetCustomerByAppUserIdQueryResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
