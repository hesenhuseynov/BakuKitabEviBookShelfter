using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.ForgotPassword
{
    public  class ForgotPasswordCommandRequest:IRequest<ForgotPasswordCommandResponse>
    {
        public string Email { get; set; }

    }
}
