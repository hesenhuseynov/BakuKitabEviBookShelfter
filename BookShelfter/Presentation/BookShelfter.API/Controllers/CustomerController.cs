using BookShelfter.Application.Features.Commands.Category;
using BookShelfter.Application.Features.Queries.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController:ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{appUserId}")]
    public async Task<IActionResult> GetCustomerByAppUserId(string appUserId)
    {
        var request = new GetCustomerByAppUserIdQueryRequest { UserId = appUserId };
        var response = await _mediator.Send(request);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }







}