using BookShelfter.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.AppUser
{
    public class GetUserProfileQueryHandler:IRequestHandler<GetUserProfileQueryRequest,GetUserProfileQueryResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public GetUserProfileQueryHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            Task<string> some;

            if (user == null)
            {
                return new GetUserProfileQueryResponse()
                {
                    Success = false,    
                    Message = "User not found"
                };

            }

            return new GetUserProfileQueryResponse
            {
                Success = true,
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NameSurName = user.NameSurName
            };
        }
    }
}
