using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using MediatR;

namespace BookShelfter.Application.Features.Commands.AppUser.CreateUser
{
    public  class CreateUserCommandHandler:IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
    {

        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async  Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameUserName,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.UserName

            });


            return new()
            {
                Message = response.Message,
                Suceeded = response.Succeeded,
                
            };

        }
    }
}
