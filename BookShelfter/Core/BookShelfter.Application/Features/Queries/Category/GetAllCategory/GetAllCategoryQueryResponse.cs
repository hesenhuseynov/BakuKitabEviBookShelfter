namespace BookShelfter.Application.Features.Queries.Category.GetAllCategory;

public class GetAllCategoryQueryResponse
{
    public int TotalCount { get; set; }
    public object? Category { get; set; }
    public ICollection<Domain.Entities.Book>  books  { get; set; }
    
}