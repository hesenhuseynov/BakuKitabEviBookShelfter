using BookShelfter.Application.Repositories.Order;
using BookShelfter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Persistence.Contexts;

namespace BookShelfter.Persistence.Repositories.Order
{
    public  class OrderWriteRepository:WriteRepository<Domain.Entities.Order>,IOrderWriteRepository
    {
        public OrderWriteRepository(BookShelfterDbContext context) : base(context)
        {
        }
    }
}
