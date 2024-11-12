using System.Runtime.InteropServices.JavaScript;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.ViewModels.Books;
using Google.Apis.Storage.v1.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookShelfter.Application.Features.Commands.Role.CreateRole;

public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommandRequest,CreateRoleCommandResponse>
{
    public readonly IRoleService roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    public async  Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await this.roleService.CreateRoleAsync(request.Name, request.Description);
        if (result.Succeeded)
        {
            return new()
            {
                Succes = true,
                Message = "Role created Succeffuly"
            };

        }


        return new()
        {
            Succes = false,
            Message = "Role Creation Failed",
            Errors = result.Errors.Select(c => c.Description).ToList()
        };
    }
    
}