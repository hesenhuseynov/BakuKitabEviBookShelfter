using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.ResetPassword
{
    public  class ResetPasswordCommandHandler:IRequestHandler<ResetPasswordCommandRequest,ResetPasswordCommandResponse>
    {

        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new ResetPasswordCommandResponse()
                {
                    Success = false,
                    Message = "Invalid email adress"
                };


            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!resetPassResult.Succeeded)
            {
                return new()
                {
                    Success = false,
                    Message = "Password reset failed. Please ensure the token is valid and the new password meets requirements"
                };
            }


            return new()
            {
                Success = true,
                Message = "Password reset successfull"
            };
        }
    }
}
