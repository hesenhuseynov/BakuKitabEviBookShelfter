using BookShelfter.Application.Repositories.Book;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BookShelfter.Persistence.Book;

public class BookReadRepository : ReadRepository<Domain.Entities.Book>, IBookReadRepository
{
    private readonly BookShelfterDbContext _context;
    private readonly ILogger _logger;

    public BookReadRepository(BookShelfterDbContext context, ILogger logger) : base(context)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<Domain.Entities.Book?> GetBookByIdWithImagesAsync(int id)
    {
        var book = await _context.Books.Include(b => b.BookImagesFile)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
        {
            _logger.Information($"Kitap ID {id} ile kitab tapila bilmedi.");

            return null;
        }

        return book;

    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetAllBooksWithImagesAsync(int pageNumber, int pageSize)
    {
        return await _context.Books.Include(b => b.BookImagesFile)
            .OrderBy(b=>b.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetFeaturedBooksAsync()
    {
        return await _context.Books.Include(b => b.BookImagesFile)
        .AsNoTracking()
            .Where(b => b.IsFeatured)
            .ToListAsync();
    }

    public async Task<int> GetTotalBooksCountAsync()
    {
        return await  _context.Books.CountAsync();
    }

    public  async Task<List<Domain.Entities.Book>> SearchBookAsync(string keyword)
    {
        return await _context.Books.Include(b => b.BookImagesFile)
            .Where(b => b.BookName.Contains(keyword) || b.AuthorName.Contains(keyword))
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetNewArrivalsAsync()
    {
        return await _context.Books
        .AsNoTracking()
            .Include(c => c.BookImagesFile)
            .OrderByDescending(b => b.CreatedDate)
            .Take(8)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetMostViewedBookAsync()
    {
        return await _context.Books
        .AsNoTracking()
            .Include(b => b.BookImagesFile)
            .OrderByDescending(b => b.ViewCount)
            .Take(8)
            .ToListAsync();

    }


    public async Task<Domain.Entities.Book> GetBookByDetailsAsync(string bookName, string authorName)
    {
        return await _context.Books
            .Include(b => b.BookImagesFile)
            .FirstOrDefaultAsync(b => b.BookName == bookName && b.AuthorName == authorName);

    }

    public Task<IEnumerable<Domain.Entities.Book>> GetBooksByAuthorAsync(string authorName)
    {
        throw new NotImplementedException();

    }

    public async Task<bool> IsDuplicateImageAsync(string bookName, string authorName, List<string> imageUrls)
    {
        var book = await GetBookByDetailsAsync(bookName, authorName);
        if (book != null)
        {
            var duplicateImages = book.BookImagesFile?.Where(img => imageUrls.Contains(img.ImageUrl)).ToList();
            return duplicateImages != null && duplicateImages.Count > 0;

        }

        return false;
    }

    public async Task<IEnumerable<Domain.Entities.Book?>> GetRelatedBooksByCategoryAsync(int categoryId)
    {
        //return await    _context.Books.Where(b => b.CategoryId == categoryId).ToListAsync();

        return await _context.Books
            .Include(b => b.BookImagesFile)
            .Where(b => b.CategoryId == categoryId)
            .ToListAsync();


    }
}