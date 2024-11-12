using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetAllBook;

public class GetAllBookQueryRequest:IRequest<GetAllBookQueryResponse>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}