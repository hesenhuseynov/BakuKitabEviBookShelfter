using BookShelfter.Application.Features.Commands.Role.AssignRole;
using BookShelfter.Application.Features.Commands.Role.CreateRole;
using BookShelfter.Application.Features.Commands.Role.RemoveRoleFromUser;
using BookShelfter.Application.Features.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
public class RolesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Succes
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("Assign")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Succes
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetRolesQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("RemoveRoleFromUser")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleFromUserRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Succes
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }
}
