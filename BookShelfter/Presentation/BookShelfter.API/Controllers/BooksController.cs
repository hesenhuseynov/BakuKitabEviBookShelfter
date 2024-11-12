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

//[EnableRateLimiting("Basic")]
public class BooksController(IMediator mediator, ILogger<BooksController> logger) : ControllerBase
{
    private readonly ILogger<BooksController> _logger = logger;



    //[HttpPost("create-and-upload")]
    //public async Task<IActionResult> CreateBookWithImage([FromForm] CreateBookWithImageCommandRequest request)
    //{
    //    CreateBookWithImageCommandResponse response = await _mediator.Send(request);
    //    return Ok(response);
    //}

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string keyword)
    {
        var query = new SearchBooksQueryRequest { Keyword = keyword };
        var result = await mediator.Send(query);
        return Ok(result);
    }
        

    [HttpGet("new-arrivals")]
    public async Task<IActionResult> GetNewArrivalsBooks([FromQuery] GetNewArrivalsBooksQueryRequest getNewArrivalsBooksQuery)
    {
        var response = await mediator.Send(getNewArrivalsBooksQuery);

        return Ok(response);
    }


    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedBooks([FromQuery] GetFeaturedBooksQueryRequest getFeaturedBooksQuery)
    {
        var response = await mediator.Send(getFeaturedBooksQuery);
        return Ok(response);
    }



    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewedBooks([FromQuery] GetMostViewedBooksQueryRequest getMostViewedBooksQuery)
    {
        var response = await mediator.Send(getMostViewedBooksQuery);
        return Ok(response);
        
    }




    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBookQueryRequest getAllBookQueryRequest)
    {
        GetAllBookQueryResponse response = await mediator.Send(getAllBookQueryRequest);
        return Ok(response);

    }



    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBookQueryRequest getByIdBookQueryRequest)
    {
        GetByIdBookQueryResponse response = await mediator.Send(getByIdBookQueryRequest);

        return Ok(response);

    }




    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommandRequest bookCommandRequest)
    {

        logger.LogInformation("Created Endpoint called");
        CreateBookCommandResponse response = await mediator.Send(bookCommandRequest);

        if (!response.Success)
        {
            logger.LogWarning("Failed to create book. Reason: {Reason}", response.Message);
            return BadRequest(response);
        }

        _logger.LogInformation("Book created successfully.");

        return Ok(response);

    }


    [HttpPut("updateBook")]
    public async  Task<IActionResult> UpdateBook( [FromBody] UpdateBookCommandRequest request)
    {

        var response = await  mediator.Send(request);

         if(response.Success)
              return Ok(response);


        
         return BadRequest(response);


    }



    [HttpPost("mark-as-featured")]
    public async Task<IActionResult> MarkAsFeatured([FromBody] MarkBookAsFeaturedCommandRequest request)
    {
        var response=await mediator.Send(request);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);

    }


    [HttpPost("incremenet-view-count")]
    public async Task<IActionResult> InCrementViewCount([FromBody] IncrementBookViewCountCommandRequest request)
    {
         var response =await mediator.Send(request);
         if (response.IsSuccess)
         {
             return Ok(response);
         }

         return BadRequest();

    }





    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]

    public async Task<IActionResult> RemoveById([FromRoute] int id)
    {

        logger.LogInformation("Received request to delete book with id: {Id}", id);


        var request = new RemoveProductCommandRequest { Id = id };
        RemoveProductCommandResponse response = await mediator.Send(request);

        if (!response.Success)  
        {
            logger.LogWarning("Failed to delete book with id: {Id}. Reason: {Reason}", id, response.Message);

            return BadRequest(response);
        }
        logger.LogInformation("Successfully deleted book with id: {Id}", id);

        return Ok(response);

    }


    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("uploadBookImage")]
    public async Task<IActionResult> UploadBookImage(IFormFile? file, [FromForm] int bookId)
    {

        if (file == null || file.Length == 0)
            return BadRequest("No File uploaded");

        var command = new UploadBookImageCommandRequest(bookId, file);
        var response = await mediator.Send(command);
        if (response.Success)
        {
            return Ok(new { Message = response.Message, ImageUrl = response.ImageUrl });
        }

        return BadRequest(new { Message = response.Message });
        
    }


    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("deleteImage")]
    public async Task<IActionResult> DeleteImage([FromQuery] string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return BadRequest("No  image Url provided");

        var command = new RemoveBookImageCommandRequest(imageUrl);
        var response = await mediator.Send(command);

        if (response.Success)
            return Ok(new { Message = response.Message });

        return BadRequest(new { Message = response.Message });

    }



    [HttpGet("{bookId}/details")]
    public async Task<IActionResult> GetBookDetails( [FromRoute]int bookId)
    {
        var request = new GetBookDetailsQueryRequest { BookId = bookId };
        var response= await mediator.Send(request);

        if (response.Success)
        {
            return Ok(response);
        }

        return NotFound(response); 

    }




    [HttpGet("related/{categoryId}")]

    public  async Task<IActionResult> GetRelatedBooksByCategory([FromRoute]   int categoryId  )
    {

        var request = new GetRelatedBooksByCategoryQueryRequest { CategoryId = categoryId };
        var response = await  mediator.Send(request);

        if (response.Success)
        {
            return Ok(response);

        }

        return BadRequest(response);
    }








}


