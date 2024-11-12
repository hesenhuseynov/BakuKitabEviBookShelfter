using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Book.GetAllBook;

public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQueryRequest, GetAllBookQueryResponse>
{
    private readonly IBookReadRepository _bookReadRepository;
    private readonly ICacheService _cacheService;

    public GetAllBookQueryHandler(IBookReadRepository bookReadRepository, ICacheService cacheService)
    {
        _bookReadRepository = bookReadRepository;
        _cacheService = cacheService;
    }
    public async Task<GetAllBookQueryResponse> Handle(GetAllBookQueryRequest request, CancellationToken cancellationToken)
    {

        // string cacheKey = "all_books_cache";
        //string cacheKey = $"all_books_cache_page_{request.PageNumber}_size_{request.PageSize}";

        //if (_cacheService.TryGetValue(cacheKey, out List<BookDto> cachedBooks))
        //{
        //    return new GetAllBookQueryResponse
        //    {
        //        Books = cachedBooks,
        //        TotalProductCount = cachedBooks.Count
        //    };
        //}
        var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
        var pageSize = request.PageSize > 0 ? request.PageSize : 10;


        var allbooks = await  _bookReadRepository.GetAllBooksWithImagesAsync(pageNumber, pageSize);

        var totalbooksCount = await _bookReadRepository.GetTotalBooksCountAsync();
 

        //var totalProductCount = allbooks.Count();

        var booksDtos = allbooks.Select(book => new BookDto()
        {    BookId =book.Id,
            BookName = book.BookName,   
            AuthorName = book.AuthorName,
            Stock = book.Stock,
            Price = book.Price,
            Description = book.Description,
            CategoryId = book.CategoryId,
            ImageUrls = book.BookImagesFile.Select(img=>img.ImageUrl).ToList(),

        }).ToList();


        //_cacheService.Set(cacheKey,booksDtos,TimeSpan.FromMinutes(30));

        return new()
        {
            Books= booksDtos,
            TotalProductCount = totalbooksCount
        };

    }
}