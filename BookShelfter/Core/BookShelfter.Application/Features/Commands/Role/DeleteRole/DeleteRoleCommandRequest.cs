using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Role.DeleteRole
{
    public  class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string Name  { get; set; }


    }
}
