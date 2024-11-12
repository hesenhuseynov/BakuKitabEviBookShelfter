using BookShelfter.Application.Repositories.Category;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Category;

public class CategoryReadRepository:ReadRepository<Domain.Entities.Category>,ICategoryReadRepository
{
    public CategoryReadRepository(BookShelfterDbContext context) : base(context)
    {
    }

    public  async Task<ICollection<Domain.Entities.Book>> GetBooksByCategoryId(string categoryId)
    {
        if (string.IsNullOrEmpty(categoryId))
        {
          throw new ArgumentNullException("CategoryId ID cannot be null or empty",nameof(categoryId));
            

        }

        if (!int.TryParse( categoryId,out int categoryIDInt))
        {

            throw new ArgumentException("Invalid Category Id format", nameof(categoryId));

        }


        //var query = _context.Books.Where(b => b.CategoryId == Convert.ToInt32(categoryId));

        //return await query.ToListAsync();


        var books = await _context.Books
            .Include(b => b.Category)
            .Include(b => b.BookImagesFile)
            .Include(b => b.BasketItems)
            .Include(b => b.Reviews)
            .Where(b => b.CategoryId == categoryIDInt)
            .AsSplitQuery()
            .ToListAsync();




        return books;



    }
}