using BookShelfter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.AppUser.RefreshToken
{
    public  class RefreshTokenCommandResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Token token { get; set; }
    }
}
