using System.Security.Claims;
using BookShelfter.Application.Features.Queries.AppUser;
using BookShelfter.Application.Features.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var query = new GetUserProfileQueryRequest { UserId = userId };
        var response = await _mediator.Send(query);

        return response.Success
            ? Ok(new
            {
                response.UserId,
                response.UserName,
                response.Email,
                response.NameSurName
            })
            : BadRequest(new { message = response.Message });
    }

    [HttpGet("panel")]
    public IActionResult GetUserPanel()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        if (roles.Contains("Admin"))
            return Ok(new { UserId = userId, Panel = "Admin Panel" });

        if (roles.Contains("User"))
            return Ok(new { UserId = userId, Panel = "User Panel" });

        return Forbid();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Sucess
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string id)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (id != currentUserId && !isAdmin)
            return Forbid();

        var query = new GetUserByIdQueryRequest { UserId = id };
        var response = await _mediator.Send(query);

        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommandRequest command)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");

        if (id != currentUserId && !isAdmin)
            return Forbid();

        command.UserId = id;
        var response = await _mediator.Send(command);

        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var command = new DeleteUserCommandRequest { UserId = id };
        var response = await _mediator.Send(command);

        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }
}
