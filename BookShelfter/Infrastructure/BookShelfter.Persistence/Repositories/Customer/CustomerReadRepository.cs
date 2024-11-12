using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application;
using BookShelfter.Application.Repositories.Customer;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.Customer
{
    public  class CustomerReadRepository:ReadRepository<Domain.Entities.Customer>,ICustomerReadRepository
    {
        public CustomerReadRepository(BookShelfterDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.Customer?> GetCustomerByAppUserIdAsync(string appUserId)
        {
          return   await _context.Customers.SingleOrDefaultAsync(c => c.AppUserId == appUserId);
        }
    }
}
