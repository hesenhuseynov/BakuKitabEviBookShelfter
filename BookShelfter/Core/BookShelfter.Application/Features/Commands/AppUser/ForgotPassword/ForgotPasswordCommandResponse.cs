using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.ForgotPassword
{
    public class ForgotPasswordCommandResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
