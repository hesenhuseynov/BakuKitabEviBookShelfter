using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Persistence.Contexts;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.Basket
{
    internal class BasketReadRepository:ReadRepository<Domain.Entities.Basket>,IBasketReadRepository
    {
        public BasketReadRepository(BookShelfterDbContext context) : base(context)
        {
        }

        public  async Task<Domain.Entities.Basket?> GetByUserIdAsync(string userId)
        {
            return await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Book)
                .ThenInclude(b => b.BookImagesFile)
                .FirstOrDefaultAsync(c => c.UserId == userId);




        }

        public  async Task<Domain.Entities.Basket> GetBasketByUserIdAsync(string userId)
        {
            return await _context.Baskets
                .Include(b => b.User)
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Book)
                .ThenInclude(b=>b.BookImagesFile)
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }
}
