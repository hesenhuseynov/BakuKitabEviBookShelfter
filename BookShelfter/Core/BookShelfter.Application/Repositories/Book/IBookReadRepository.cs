using BookShelfter.Application.DTOs.Book;

namespace BookShelfter.Application.Repositories.Book;

public interface IBookReadRepository:IReadRepository<Domain.Entities.Book>
{
    Task<Domain.Entities.Book?> GetBookByIdWithImagesAsync(int id);

    Task  <IEnumerable<BookShelfter.Domain.Entities.Book>> GetAllBooksWithImagesAsync(int pageNumber,int pageSize);

    Task<IEnumerable<BookShelfter.Domain.Entities.Book>> GetFeaturedBooksAsync();
    Task<int> GetTotalBooksCountAsync();
    Task<List<Domain.Entities.Book>> SearchBookAsync(string keyword);
    Task<IEnumerable<BookShelfter.Domain.Entities.Book>> GetNewArrivalsAsync();

    Task<IEnumerable<BookShelfter.Domain.Entities.Book>> GetMostViewedBookAsync();


    Task<Domain.Entities.Book> GetBookByDetailsAsync(string bookName, string authorName);
    Task<IEnumerable<Domain.Entities.Book>> GetBooksByAuthorAsync(string authorName);

    Task<bool> IsDuplicateImageAsync(string bookName, string authorName,  List<string> imageUrls);

    Task<IEnumerable<Domain.Entities.Book?>> GetRelatedBooksByCategoryAsync(int categoryId);
     
    

}