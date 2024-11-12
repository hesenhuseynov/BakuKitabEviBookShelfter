using BookShelfter.Application.Features.Commands.Basket.AddItem;
using BookShelfter.Application.Features.Commands.Basket.ClearBasket;
using BookShelfter.Application.Features.Commands.Basket.RemoveItemFromBasket;
using BookShelfter.Application.Features.Queries.Basket;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddItemToBasketCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
                return Ok(response.UpdatedBasket);


            return BadRequest(response.Message);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasketByUser([FromRoute] string userId)
        {


            var request = new GetBasketByUserIdQueryRequest() { UserId = userId };

            var response = await _mediator.Send(request);

            if (response.Success)
            {

                return Ok(response);
            }

            return BadRequest(response);

        }


        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItemFromBasket([FromBody] RemoveItemFromBasketCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);

        }


        [HttpPost("clear")]

        public async Task<IActionResult> ClearAllBasket([FromBody] ClearBasketCommandRequest request)
        {
            var response =  await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
                
            }

            return BadRequest(response);
        }

         
    }
}
