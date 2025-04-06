using BookShelfter.Application.Features.Commands.Category;
using BookShelfter.Application.Features.Queries.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{appUserId}")]
    public async Task<IActionResult> GetCustomerByAppUserId([FromRoute] string appUserId)
    {
        var request = new GetCustomerByAppUserIdQueryRequest { UserId = appUserId };
        var response = await _mediator.Send(request);

        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }
}
