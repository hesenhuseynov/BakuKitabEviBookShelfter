using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs;

namespace BookShelfter.Application.Features.Commands.AppUser.RegisterUser
{
    public  class  RegisterUserCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public Token Token { get; set; }

    }
}
