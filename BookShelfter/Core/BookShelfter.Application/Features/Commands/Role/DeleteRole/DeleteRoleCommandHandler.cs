using BookShelfter.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Role.DeleteRole
{
    public  class DeleteRoleCommandHandler:IRequestHandler<DeleteRoleCommandRequest,DeleteRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(request.Name);
            if (result.Succeeded)
            {
                return new()
                {
                    Succes = true,
                    Message = "Role deleted successffuly"
                };



            }

            return new DeleteRoleCommandResponse
            {
                Succes = false,
                Message = "Role deletion failed.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        }
    }
}
