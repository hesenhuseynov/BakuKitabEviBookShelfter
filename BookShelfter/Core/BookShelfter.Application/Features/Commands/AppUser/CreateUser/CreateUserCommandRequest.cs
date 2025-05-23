﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.CreateUser
{
    public  class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
    {
         public string NameUserName { get; set; }

         public string UserName { get; set; }

         public string Email { get; set; }

         public string Password { get; set; }
         public string PasswordConfirm { get; set; }
        

    }
}
