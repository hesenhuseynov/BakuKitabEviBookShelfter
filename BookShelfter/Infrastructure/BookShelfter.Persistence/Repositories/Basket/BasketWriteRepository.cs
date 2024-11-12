
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.Basket
{
    public class BasketWriteRepository : WriteRepository<Domain.Entities.Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(BookShelfterDbContext context) : base(context)
        {
        }

        public async Task<bool> RemoveItemFromBasketAsync(string userId, int bookId)
        {

           await  using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var basket = await _context.Baskets.Include(c => c.BasketItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);


                if (basket == null)
                {
                    return false;


                }

                //var basketItem =   basket.BasketItems.FirstOrDefault(bi => bi.ProductId == bookId);



                var basketItem = await _context.Baskets
                    .Where(b => b.UserId == userId)
                    .SelectMany(b => b.BasketItems)
                    .FirstOrDefaultAsync(bi => bi.BookId == bookId);





                if (basketItem == null)
                {
                    return false;

                }


                basket.BasketItems.Remove(basketItem);


                await _context.SaveChangesAsync();


                await transaction.CommitAsync();


                return true;
            }


            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                Console.WriteLine($"An error occurred while removing product with ID {bookId} from user's basket: {userId}. Error: {ex.Message}");
                return false;

            }



        }

        public async Task<bool> ClearAllBasketAsync(string userId)
        {
            var basket = await  _context.Baskets.Include(c => c.BasketItems)
                .FirstOrDefaultAsync(u => u.UserId == userId);


            if (basket == null || !basket.BasketItems.Any())
            {

                return false;

            }


            basket.BasketItems.Clear();

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
