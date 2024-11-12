using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.LoginUser
{
    public  class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
    {
        public string UserName  { get; set; }
        public string  Password { get; set; }
    }
}
