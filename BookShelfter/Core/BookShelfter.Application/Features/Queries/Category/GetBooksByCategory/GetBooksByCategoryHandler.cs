using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.DTOs.Category;
using BookShelfter.Application.Repositories.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;

public class GetBooksByCategoryHandler : IRequestHandler<GetBooksByCategoryQueryRequest, GetBooksByCategoryQueryResponse>
{

    private readonly ICategoryReadRepository _categoryReadRepository;

    public GetBooksByCategoryHandler(ICategoryReadRepository categoryReadRepository)
    {
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<GetBooksByCategoryQueryResponse> Handle(GetBooksByCategoryQueryRequest request, CancellationToken cancellationToken)
    {


        try
        {

            var books = await _categoryReadRepository.GetBooksByCategoryId(request.CategoryId);

            if (books == null || !books.Any())
            {
                return new()
                {
                    Success = false,
                    Message = "No books found for the given category"
                };

            }


            var booksDto = books.Select(b => new BookDto
            {
                BookId = b.Id,
                BookName = b.BookName,
                AuthorName = b.AuthorName,
                Stock = b.Stock,
                Price = b.Price,
                Description = b.Description,
                CategoryId = b.CategoryId,
                ImageUrls = b.BookImagesFile.Select(c=>c.ImageUrl).ToList()

            }).ToList();

            return new GetBooksByCategoryQueryResponse
            {
                Success = true,
                Message = "Books successfully retrieved for the category.",
                BooksDto = booksDto
            };



        }

        catch (Exception e)
        {
            return new GetBooksByCategoryQueryResponse()
            {
                Success = false,
                Errors = new List<string>() { e.Message },

                Message = "An error occurred while retrieving books by category."


            };



        }




    }
}