using MediatR;

namespace BookShelfter.Application.Features.Commands.Role.CreateRole;

public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
}