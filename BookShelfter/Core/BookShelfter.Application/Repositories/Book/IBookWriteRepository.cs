namespace BookShelfter.Application.Repositories.Book;

public interface IBookWriteRepository:IWriteRepository<Domain.Entities.Book>
{

    Task<bool> MarkBookAsFeaturedAsync(int bookId);
    Task<bool> IncrementBookViewCountAsync(int bookId);



}