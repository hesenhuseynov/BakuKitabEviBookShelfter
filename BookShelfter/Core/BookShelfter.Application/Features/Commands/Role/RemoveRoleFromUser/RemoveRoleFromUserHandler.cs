using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Role.RemoveRoleFromUser
{
    public class RemoveRoleFromUserHandler : IRequestHandler<RemoveRoleFromUserRequest, RemoveRoleFromUserResponse>
    {
        public readonly IRoleService _roleService;

        public RemoveRoleFromUserHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RemoveRoleFromUserResponse> Handle(RemoveRoleFromUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.RemoveRoleFromUserAsync(request.userId, request.roleName);

            if (result.Succeeded)
            {
                return new()
                {
                    Message = "Role remove to user succesffulty",
                    Succes = true
                };

            }


            return new()
            {
                Message = "not succeffuly remove role to user",
                Succes = false
            };

        }
    }
}
