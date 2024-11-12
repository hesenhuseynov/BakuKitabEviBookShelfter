using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Role.RemoveRoleFromUser
{
    public class RemoveRoleFromUserRequest : IRequest<RemoveRoleFromUserResponse>
    {
        public string userId { get; set; }  

        public string roleName { get; set; }
    }
}
