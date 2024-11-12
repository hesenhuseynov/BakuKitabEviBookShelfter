 using System.ComponentModel.DataAnnotations;
 using System.Management;
 using BookShelfter.Application.Common;
 using BookShelfter.Application.Features.Commands.AppUser.Email;
 using BookShelfter.Application.Features.Commands.AppUser.ForgotPassword;
 using BookShelfter.Application.Features.Commands.AppUser.GoogleLogin;
 using BookShelfter.Application.Features.Commands.AppUser.LoginUser;
 using BookShelfter.Application.Features.Commands.AppUser.RefreshToken;
 using BookShelfter.Application.Features.Commands.AppUser.RegisterUser;
 using BookShelfter.Application.Features.Commands.AppUser.ResetPassword;
 using MediatR;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using Serilog;
using IResult = BookShelfter.Application.Common.IResult;

namespace BookShelfter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest command)
        {
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest command)
        {
              var response= await mediator.Send(command);

              if (response.Success)
              {
                  return Ok(response);
              }

              if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("Email not confirmed"))
              {
                return Ok(response.Message);
            }


            return BadRequest(response);
            
        }
        

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginUserCommandRequest request)
        {
            var response=  await  mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);    

        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommandRequest command)
        {
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response.Message);
                //return Redirect("/ResetPassword.html");
            }

            return BadRequest(response.Message);
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest command)
        {
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);
        }


        [HttpPost("RefreshToken")]
        public  async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest command)
        {
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


      
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            var command = new ConfirmEmailCommandRequest { Token = token, Email = email };
            var response = await mediator.Send(command);
            if (response.Succes)
            {
                return Redirect(response.RedirectUrl);
            }
            //if (response.Succes)
            //{
            //    return Ok(new { Success = true, Message = "Email confirmed successfully" });
            //}



            return BadRequest(new { Success = false, Message = "Email confirmation failed" });


            //return BadRequest(response);
        }

    }
}
