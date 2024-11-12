using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.ResetPassword
{
    public  class ResetPasswordCommandResponse
    {
        public bool Success { get; set; }

        public  string Message { get; set; }
    }
}
