using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.Email
{
    public  class ConfirmEmailCommandRequest:IRequest<ConfirmEmailCommandResponse>
    {

        public string Token { get; set; }

        public string Email { get; set; }

    }
}
