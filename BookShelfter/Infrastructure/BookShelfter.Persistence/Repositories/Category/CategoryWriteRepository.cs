using BookShelfter.Application.Repositories.Category;
using BookShelfter.Persistence.Contexts;

namespace BookShelfter.Persistence.Category;

public class CategoryWriteRepository:WriteRepository<Domain.Entities.Category>,ICategoryWriteRepository
{
    public CategoryWriteRepository(BookShelfterDbContext context) : base(context)
    {
    }
}