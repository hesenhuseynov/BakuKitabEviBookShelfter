using BookShelfter.Application.DTOs.Book;

namespace BookShelfter.Application.Features.Queries.Book.GetAllBook;

public class GetAllBookQueryResponse
{
    public int TotalProductCount { get; set; }
    public List<BookDto> Books { get; set; }
}