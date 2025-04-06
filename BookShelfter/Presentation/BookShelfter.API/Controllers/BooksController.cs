using System.Net;
using BookShelfter.Application.Features.Commands.Book.CreateBook;
using BookShelfter.Application.Features.Commands.Book.CreateBookWithImage;
using BookShelfter.Application.Features.Commands.Book.IncrementBookView;
using BookShelfter.Application.Features.Commands.Book.MarkBookAsFeatured;
using BookShelfter.Application.Features.Commands.Book.RemoveBookImage;
using BookShelfter.Application.Features.Commands.Book.RemoveProduct;
using BookShelfter.Application.Features.Commands.Book.UpdateBook;
using BookShelfter.Application.Features.Commands.Book.UploadBookImage;
using BookShelfter.Application.Features.Queries.Book.GetAllBook;
using BookShelfter.Application.Features.Queries.Book.GetBookDetails;
using BookShelfter.Application.Features.Queries.Book.GetById;
using BookShelfter.Application.Features.Queries.Book.GetFeaturedBooks;
using BookShelfter.Application.Features.Queries.Book.GetMostViewedBooks;
using BookShelfter.Application.Features.Queries.Book.GetNewArrivalsBooks;
using BookShelfter.Application.Features.Queries.Book.GetRelatedBooksByCategory;
using BookShelfter.Application.Features.Queries.Book.SearchBooksQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookShelfter.API.Controllers;



[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")] // Bütün metodlar default qorunur
public class BooksController(IMediator mediator, ILogger<BooksController> logger) : ControllerBase
{
    private readonly ILogger<BooksController> _logger = logger;

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string keyword)
    {
        var query = new SearchBooksQueryRequest { Keyword = keyword };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("new-arrivals")]
    public async Task<IActionResult> GetNewArrivalsBooks([FromQuery] GetNewArrivalsBooksQueryRequest query)
        => Ok(await mediator.Send(query));

    [AllowAnonymous]
    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedBooks([FromQuery] GetFeaturedBooksQueryRequest query)
        => Ok(await mediator.Send(query));

    [AllowAnonymous]
    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewedBooks([FromQuery] GetMostViewedBooksQueryRequest query)
        => Ok(await mediator.Send(query));

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBookQueryRequest query)
        => Ok(await mediator.Send(query));

    [AllowAnonymous]
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBookQueryRequest query)
        => Ok(await mediator.Send(query));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommandRequest request)
    {
        _logger.LogInformation("Creating book...");
        var response = await mediator.Send(request);
        if (!response.Success)
        {
            _logger.LogWarning("Failed to create book: {Reason}", response.Message);
            return BadRequest(response);
        }
        _logger.LogInformation("Book created.");
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("updateBook")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommandRequest request)
    {
        var response = await mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("mark-as-featured")]
    public async Task<IActionResult> MarkAsFeatured([FromBody] MarkBookAsFeaturedCommandRequest request)
    {
        var response = await mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("incremenet-view-count")]
    public async Task<IActionResult> InCrementViewCount([FromBody] IncrementBookViewCountCommandRequest request)
    {
        var response = await mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveById([FromRoute] int id)
    {
        _logger.LogInformation("Deleting book {Id}", id);
        var request = new RemoveProductCommandRequest { Id = id };
        var response = await mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("uploadBookImage")]
    public async Task<IActionResult> UploadBookImage(IFormFile? file, [FromForm] int bookId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No File uploaded");

        var command = new UploadBookImageCommandRequest(bookId, file);
        var response = await mediator.Send(command);

        return response.Success
            ? Ok(new { response.Message, response.ImageUrl })
            : BadRequest(new { response.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("deleteImage")]
    public async Task<IActionResult> DeleteImage([FromQuery] string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return BadRequest("No image Url provided");

        var command = new RemoveBookImageCommandRequest(imageUrl);
        var response = await mediator.Send(command);

        return response.Success
            ? Ok(new { response.Message })
            : BadRequest(new { response.Message });
    }

    [AllowAnonymous]
    [HttpGet("{bookId}/details")]
    public async Task<IActionResult> GetBookDetails([FromRoute] int bookId)
    {
        var request = new GetBookDetailsQueryRequest { BookId = bookId };
        var response = await mediator.Send(request);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [AllowAnonymous]
    [HttpGet("related/{categoryId}")]
    public async Task<IActionResult> GetRelatedBooksByCategory([FromRoute] int categoryId)
    {
        var request = new GetRelatedBooksByCategoryQueryRequest { CategoryId = categoryId };
        var response = await mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    }


