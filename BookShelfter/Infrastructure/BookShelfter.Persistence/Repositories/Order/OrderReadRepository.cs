using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Order;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.Order
{
    public class OrderReadRepository:ReadRepository<Domain.Entities.Order>,IOrderReadRepository
    {
        public OrderReadRepository(BookShelfterDbContext context) : base(context)
        {
        }

        public  async Task<Domain.Entities.Order?> GetOrderWithDetailsByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(c => c.OrderDetails)
                .ThenInclude(od => od.Book)
                .ThenInclude(b=>b.BookImagesFile)
                .FirstOrDefaultAsync(o => o.Id == orderId);


        }
    }
}
