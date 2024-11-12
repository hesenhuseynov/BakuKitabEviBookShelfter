using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Features.Commands.AppUser.Email
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, ConfirmEmailCommandResponse>
    {
        public readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public ConfirmEmailCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenService tokenService, IAuthService authService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authService = authService;
        }

        public async  Task<ConfirmEmailCommandResponse> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {

            #region lastVersion


            //var normalizedEmail = request.Email.ToLowerInvariant();
            //var user = await _userManager.FindByEmailAsync(normalizedEmail);


            ////var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email);


            //if (user == null)
            //{

            //    return new()
            //    {
            //        Succes = false,
            //        Message = "Invalid email adress"

            //    };
            //}

            //var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            //if (result.Succeeded)
            //{
            //    user.EmailConfirmed = true;
            //    await _userManager.UpdateAsync(user);

            //    var roles = await _userManager.GetRolesAsync(user);
            //    var jwtToken = _tokenService.CreateAccessToken(30, user, roles);

            //    return new ConfirmEmailCommandResponse
            //    {
            //        Succes = true,
            //        Message = "Email confirmed successfully. you can Login right now ",
            //        Data = jwtToken,
            //    };


            //}


            //return new ConfirmEmailCommandResponse
            //{
            //    Succes = false,
            //    Message = "Email confirmation failed.Invali token or token has expired"
            //};
            #endregion

            var result = await _authService.ConfirmEmailAsyncAndCreateToken(request.Token, request.Email, 30);

            if (result.Succes)
            {
                var redirectResult = result as Result;
                return new()
                {
                    Succes = true,
                    Message = result.Message,
                    RedirectUrl = redirectResult?.RedirectUrl

                };

            }


            return new()
            {
                Succes = false,
                Message = result.Message
            };



        }
    }
}
