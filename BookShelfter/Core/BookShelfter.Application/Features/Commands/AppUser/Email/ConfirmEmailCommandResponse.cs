using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;

namespace BookShelfter.Application.Features.Commands.AppUser.Email
{
    public  class ConfirmEmailCommandResponse:IResult
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }

        public Token Data { get; set; }
        public List<string> Errors { get; }
    }
}
