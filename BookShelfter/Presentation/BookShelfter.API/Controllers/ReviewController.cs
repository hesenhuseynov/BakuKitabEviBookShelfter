using BookShelfter.Application.Features.Commands.Review;
using BookShelfter.Application.Features.Queries.Review;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ReviewController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize(Roles = "User")]
    [HttpPost("AddReviews")]
    public async Task<IActionResult> Add([FromBody] AddReviewCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [AllowAnonymous]
    [HttpGet("books/{bookId}/reviews")]
    public async Task<IActionResult> GetReviewsByBookId([FromRoute] int bookId)
    {
        var request = new GetReviewsByBookIdQueryRequest { BookId = bookId };
        var response = await _mediator.Send(request);

        if (response.Reviews == null || !response.Reviews.Any())
            return NoContent();

        return Ok(response);
    }
}
