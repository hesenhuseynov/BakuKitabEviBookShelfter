using BookShelfter.Application.Features.Commands.Order.CreateOrder;
using BookShelfter.Application.Features.Commands.Order.DeleteOrder;
using BookShelfter.Application.Features.Commands.Order.UpdateOrder;
using BookShelfter.Application.Features.Queries.Order.GetAll;
using BookShelfter.Application.Features.Queries.Order.GetById;
using BookShelfter.Application.Features.Queries.Order.GetOrderWithDetailsById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetOrderWithDetailsById/{orderId}")]
    public async Task<IActionResult> GetOrderWithDetailsById([FromRoute] int orderId)
    {
        var request = new GetOrderWithDetailsByIdQueryRequest { OrderId = orderId };
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int orderId, [FromBody] UpdateOrderCommandRequest request)
    {
        request.OrderId = orderId;
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int orderId)
    {
        var request = new DeleteOrderCommandRequest { OrderId = orderId };
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
    {
        var request = new GetOrderByIdQueryRequest { OrderId = orderId };
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }
}
