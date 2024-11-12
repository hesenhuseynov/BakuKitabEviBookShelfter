using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetById;

public class GetByIdBookQueryRequest:IRequest<GetByIdBookQueryResponse>
{
    public int Id { get; set; }
    
    
    
}