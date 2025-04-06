using BookShelfter.Application.Common;
using BookShelfter.Application.Features.Commands.AppUser.Email;
using BookShelfter.Application.Features.Commands.AppUser.ForgotPassword;
using BookShelfter.Application.Features.Commands.AppUser.GoogleLogin;
using BookShelfter.Application.Features.Commands.AppUser.LoginUser;
using BookShelfter.Application.Features.Commands.AppUser.RefreshToken;
using BookShelfter.Application.Features.Commands.AppUser.RegisterUser;
using BookShelfter.Application.Features.Commands.AppUser.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] 
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest command)
    {
        var response = await mediator.Send(command);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest command)
    {
        var response = await mediator.Send(command);

        if (response.Success)
            return Ok(response);

        if (!string.IsNullOrEmpty(response.Message) &&
            response.Message.Contains("Email not confirmed", StringComparison.OrdinalIgnoreCase))
        {
            return Ok(new { message = response.Message });
        }

        return BadRequest(new { message = response.Message });
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginUserCommandRequest request)
    {
        var response = await mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommandRequest command)
    {
        var response = await mediator.Send(command);
        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest command)
    {
        var response = await mediator.Send(command);
        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest command)
    {
        var response = await mediator.Send(command);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        var command = new ConfirmEmailCommandRequest { Token = token, Email = email };
        var response = await mediator.Send(command);

        return response.Succes
            ? Redirect(response.RedirectUrl)
            : BadRequest(new { success = false, message = "Email confirmation failed" });
    }
}
