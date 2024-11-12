using System.Security.Claims;
using BookShelfter.Application.Features.Queries.AppUser;
using BookShelfter.Application.Features.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = new GetUserProfileQueryRequest { UserId = userId };

            var response = await _mediator.Send(query);


            if (response.Success)
            {
                return Ok(new
                {
                    response.UserId,
                    response.UserName,
                    response.Email  ,
                    response.NameSurName
                });

            }

            return BadRequest(response.Message);

        }

        [HttpGet("panel")]
        [Authorize]
        public IActionResult GetUserPanel()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            if (roles.Contains("Admin"))
            {
                return Ok(new { UserId = userId, Panel = "Admin Panel" });
            }
            else if (roles.Contains("User"))
            {
                return Ok(new { UserId = userId, Panel = "User Panel" });
            }

            return Forbid();

        }


        [HttpGet]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Sucess)
                return Ok(response);


            return BadRequest(response);
        }




        //[HttpGet("{id}")]
        //[Authorize("Admin")]
        //public async Task<IActionResult> GetUserById(string id)
        //{
        //    var query = new GetUserByIdQueryRequest { UserId = id };
        //    var response = await _mediator.Send(query);

        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }

        //    return BadRequest(response.Message);
        //}

        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommandRequest command)
        //{
        //    if (id != User.FindFirst(ClaimTypes.NameIdentifier)?.Value && !User.IsInRole("Admin"))
        //    {
        //        return Forbid();
        //    }

        //    command.UserId = id;
        //    var response = await _mediator.Send(command);

        //    if (response.Success)
        //    {
        //        return Ok(response.Message);
        //    }

        //    return BadRequest(response.Message);
        //}



        //[HttpDelete("{id}")]
        //[Authorize("Admin")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    var command = new DeleteUserCommandRequest { UserId = id };
        //    var response = await _mediator.Send(command);

        //    if (response.Success)
        //    {
        //        return Ok(response.Message);
        //    }

        //    return BadRequest(response.Message);
        //}


    }
}
