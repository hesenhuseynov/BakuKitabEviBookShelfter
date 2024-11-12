using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Common;

namespace BookShelfter.Application.Features.Commands.Role.DeleteRole
{
    public  class DeleteRoleCommandResponse:IResult
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
