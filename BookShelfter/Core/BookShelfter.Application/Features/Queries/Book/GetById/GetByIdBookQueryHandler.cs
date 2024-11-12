using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetById;

public class GetByIdBookQueryHandler:IRequestHandler<GetByIdBookQueryRequest,GetByIdBookQueryResponse>
{
    private readonly IBookReadRepository _bookReadRepository;
    
    public GetByIdBookQueryHandler(IBookReadRepository bookReadRepository)
    {
        _bookReadRepository = bookReadRepository;
    }
    
    public async Task<GetByIdBookQueryResponse> Handle(GetByIdBookQueryRequest request, CancellationToken cancellationToken)
    {
        
         
        // var book=  await _bookReadRepository.GetByIdAsync(request.Id, false);
        var book = await _bookReadRepository.GetBookByIdWithImagesAsync(request.Id);
        if (book ==null)
        {

            return new GetByIdBookQueryResponse
            {
                Success = false,
                Message = "Kitap Yoxdur ."
            };
        }

        var bookDto = new BookDto()
        {
            BookId = book.Id,
            BookName = book.BookName,
            AuthorName = book.AuthorName,
            Price = book.Price,
            Stock = book.Stock,
            Description = book.Description,
            CategoryId = book.CategoryId,
            ImageUrls = book.BookImagesFile.Select(img => img.ImageUrl).ToList()
        };






        return new GetByIdBookQueryResponse
        {
            Book = bookDto,
            Success = true,
            Message = "Kitap başarıyla bulundu"
        };
    }
}