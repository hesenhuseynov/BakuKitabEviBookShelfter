using BookShelfter.Application.Features.Commands.Order.CreateOrder;
using BookShelfter.Application.Features.Commands.Order.DeleteOrder;
using BookShelfter.Application.Features.Commands.Order.UpdateOrder;
using BookShelfter.Application.Features.Queries.Order.GetAll;
using BookShelfter.Application.Features.Queries.Order.GetById;
using BookShelfter.Application.Features.Queries.Order.GetOrderWithDetailsById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
                return Ok(response);


            return BadRequest(response);

        }


        [HttpGet("GetOrderWithDetailsById/{orderId}")]
        public async Task<IActionResult> GetOrderWithDetailsById([FromRoute] int orderId)
        {
            var request = new GetOrderWithDetailsByIdQueryRequest { OrderId = orderId };
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderCommandRequest request)
        {
            request.OrderId = orderId;
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);
        }



        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var request = new DeleteOrderCommandRequest { OrderId = orderId };
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);
        }





        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var request = new GetOrderByIdQueryRequest { OrderId = orderId };
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }

}

