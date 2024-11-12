using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.GoogleLogin
{
    public  class GoogleLoginUserCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
