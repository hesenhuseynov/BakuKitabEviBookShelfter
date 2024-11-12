using BookShelfter.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Role.AssignRole
{
    public  class AssignRoleCommandHandler:IRequestHandler<AssignRoleCommandRequest,AssignRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public AssignRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async  Task<AssignRoleCommandResponse> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.AssignRoleToUserAsync(request.UserId, request.RoleName);

            if (result.Succeeded)
            {
                return new()
                {
                    Succes = true,
                    Message = "Role assignewd succeffuly"
                };
            }


            return new AssignRoleCommandResponse
            {
                Succes = false,
                Message = "Role assignment failed.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}
