using BookShelfter.Application.Repositories.Book;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Book;

public class BookWriteRepository:WriteRepository<Domain.Entities.Book>,IBookWriteRepository
{
    public BookWriteRepository(BookShelfterDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetBooksByAuthorAsync(string authorName)
    {
        return await _context.Set<Domain.Entities.Book>()
            .Where(book => book.AuthorName == authorName)
            .ToListAsync();

        
    }


    public async Task<bool> MarkBookAsFeaturedAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book ==null)
        {
            return false;
        }

        book.IsFeatured = true;
        await _context.SaveChangesAsync();
        return true; 
    }

    public  async Task<bool> IncrementBookViewCountAsync(int bookId)
    {
        var book =await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            return false;
        }

        book.ViewCount++;

        await _context.SaveChangesAsync();
        return true;
    }
}