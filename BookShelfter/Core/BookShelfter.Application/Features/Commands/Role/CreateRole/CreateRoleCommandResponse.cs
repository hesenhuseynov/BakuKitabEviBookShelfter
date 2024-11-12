using BookShelfter.Application.Common;

namespace BookShelfter.Application.Features.Commands.Role.CreateRole;

public class CreateRoleCommandResponse:IResult
{
    public bool Succes { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } =new List<string>();
}