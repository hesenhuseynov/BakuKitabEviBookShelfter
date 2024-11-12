namespace BookShelfter.Application.Repositories.Category;

public interface ICategoryReadRepository:IReadRepository<Domain.Entities.Category>
{
    Task<ICollection<Domain.Entities.Book>> GetBooksByCategoryId(string  CategoryId);

}