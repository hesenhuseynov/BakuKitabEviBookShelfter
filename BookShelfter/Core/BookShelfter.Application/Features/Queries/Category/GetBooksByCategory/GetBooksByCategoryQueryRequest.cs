using MediatR;

namespace BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;

public class GetBooksByCategoryQueryRequest : IRequest<GetBooksByCategoryQueryResponse>
{
    public string CategoryId { get; set; }
    
}