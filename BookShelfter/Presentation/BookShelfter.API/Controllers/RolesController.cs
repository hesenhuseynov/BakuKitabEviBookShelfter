using BookShelfter.Application.Features.Commands.Role.AssignRole;
using BookShelfter.Application.Features.Commands.Role.CreateRole;
using BookShelfter.Application.Features.Commands.Role.RemoveRoleFromUser;
using BookShelfter.Application.Features.Queries.Role;
using BookShelfter.SignalR;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;    


        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Succes)
                return Ok(response);

            return BadRequest(response);


        }



        [HttpPost("Assign")]
        public async Task<IActionResult> AssignRoleTouser([FromBody] AssignRoleCommandRequest request)
        {
             var response=await _mediator.Send(request);
             if (response.Succes)
                 return Ok(response);

             return BadRequest(response);
            
        }




        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery]GetRolesQueryRequest request)
        {
            var response= await _mediator.Send(request);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);


        }


        [HttpPost("RemoveRoleFromUser")]
        public async  Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleFromUserRequest request)
        {
            var response=await _mediator.Send(request);
            if (response.Succes)
            {
                return Ok(response);
            }

            return BadRequest(response);
            
        }

    }
}
