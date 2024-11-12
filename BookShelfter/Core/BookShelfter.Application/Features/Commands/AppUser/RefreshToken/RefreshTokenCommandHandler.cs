using BookShelfter.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.RefreshToken
{
    public  class RefreshTokenCommandHandler:IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            return new()
            {
                Success = result.Succes,
                Message = result.Message,
                token = result.Data
            };
           
        }
    }
}
