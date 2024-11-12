using BookShelfter.Application.Features.Commands.Review;
using BookShelfter.Application.Features.Queries.Book.GetMostViewedBooks;
using BookShelfter.Application.Features.Queries.Review;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddReviews")]
        public async Task<IActionResult> Add([FromBody] AddReviewCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpGet("books/{bookId}/reviews")]
        public async Task<IActionResult> GetReviewsByBookId([FromRoute] int bookId)
        {

            var request = new GetReviewsByBookIdQueryRequest { BookId = bookId };

            var response = await _mediator.Send(request);

            if (response.Reviews == null || !response.Reviews.Any())
            {
                return NoContent();
            }

            return Ok(response);


        }
    }
}
