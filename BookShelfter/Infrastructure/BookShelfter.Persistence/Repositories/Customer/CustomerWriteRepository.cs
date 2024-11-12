using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Customer;
using BookShelfter.Persistence.Contexts;

namespace BookShelfter.Persistence.Repositories.Customer
{
    public  class CustomerWriteRepository:WriteRepository<Domain.Entities.Customer>,ICustomerWriteRepository
    {
        public CustomerWriteRepository(BookShelfterDbContext context) : base(context)
        {
        }
    }
}
