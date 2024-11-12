using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.GoogleLogin
{
    public  class GoogleLoginUserCommandRequest:IRequest<GoogleLoginUserCommandResponse>
    {
        public string IdToken { get; set; }

    }
}
