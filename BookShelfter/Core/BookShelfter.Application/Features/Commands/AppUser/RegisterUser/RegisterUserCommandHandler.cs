using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using BookShelfter.Application.Validators.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using BookShelfter.Domain.Entities.Identity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookShelfter.Application.Features.Commands.AppUser.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;


        public RegisterUserCommandHandler(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }


        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            //ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            //if (validationResult.IsValid)
            //{
            //    return new RegisterUserCommandResponse
            //    {
            //        Success = false,
            //        Message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage))

            //    };
            //}


            if (request.Password != request.PasswordConfirm)
            {
                return new RegisterUserCommandResponse
                {
                    Success = false,
                    Message =   "Password not matching",
                    Errors = new List<string> { "Password and PasswordConfirm do not match" }
                };

            }

         
            var registerDto = new RegisterDto
            {
                Email = request.Email,
                UserName = request.UserName,
                NameSurname = request.NameSurname,
                Password = request.Password
            };

             


            var result = await _authService.RegisterAsync(registerDto,50);


            if (!result.Succes)
            {
                return new RegisterUserCommandResponse
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                };
            }

            var dataResult = result as IDataResult<Token>;

            //return new()
            //{
            //    Success = true,
            //    Message = "Register is succefuly created",
            //    Token =dataResult?.Data
            //};



            return new RegisterUserCommandResponse
            {
                Success = true,
                Message = "Registration successful. Please check your email to confirm your email address.",
                Token = dataResult?.Data
            };





        }
    }
}