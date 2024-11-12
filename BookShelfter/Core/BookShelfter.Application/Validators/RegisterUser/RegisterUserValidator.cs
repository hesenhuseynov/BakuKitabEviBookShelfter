using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Commands.AppUser.RegisterUser;
using FluentValidation;

namespace BookShelfter.Application.Validators.RegisterUser
{
    public  class RegisterUserValidator:AbstractValidator<RegisterUserCommandRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email sahəsi boş buraxıla bilməz.")
                .EmailAddress().WithMessage("Etibarlı bir email ünvanı daxil edin.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("İstifadəçi adı boş buraxıla bilməz.")
                .MinimumLength(3).WithMessage("İstifadəçi adı ən azı 3 simvoldan ibarət olmalıdır.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("İstifadəçi adı yalnız hərf və rəqəmlərdən ibarət ola bilər.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə sahəsi boş buraxıla bilməz.")
                .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır.");
               

            RuleFor(x => x.PasswordConfirm)
                .NotEmpty().WithMessage("Şifrə təsdiqi sahəsi boş buraxıla bilməz.")
                .Equal(x => x.Password).WithMessage("Şifrələr uyğun gəlmir.");

        }
    }
}
