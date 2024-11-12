using System.Collections;
using BookShelfter.Application.DTOs.Book;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;

public class GetBooksByCategoryQueryResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public  ICollection<BookDto>  BooksDto { get; set; }
    

    public List<string> Errors { get; set; } = new List<string>();
}