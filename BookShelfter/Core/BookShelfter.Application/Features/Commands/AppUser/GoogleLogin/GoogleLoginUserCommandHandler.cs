using BookShelfter.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.GoogleLogin
{
    public  class GoogleLoginUserCommandHandler:IRequestHandler<GoogleLoginUserCommandRequest,GoogleLoginUserCommandResponse>
    {
        public readonly IAuthService _authService;

        public GoogleLoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public  async  Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginGoogleAsync(request.IdToken);



            if (result.Succes)
            {
                return new()
                {
                    AccessToken = result.Data.AccessToken,
                    RefreshToken = result.Data.RefreshToken,
                    Expiration = result.Data.Expiration,
                    Success = true,
                    Message = "Google Login Successfull"
                };
            }

            else
            {
                return new()
                {
                    Success = false,
                    Message = result.Message
                };
            }


            throw new NotImplementedException();
        }
    }
}
