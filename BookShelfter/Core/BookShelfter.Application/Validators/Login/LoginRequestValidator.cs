using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Commands.AppUser.LoginUser;
using FluentValidation;

namespace BookShelfter.Application.Validators.Login
{
    public class LoginRequestValidator:AbstractValidator<LoginUserCommandRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Email zəruridir");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə gərəylidir")
                .MinimumLength(6).WithMessage("Şifrə ən az 6 hərf olmalıdır");


        }

    }
}
