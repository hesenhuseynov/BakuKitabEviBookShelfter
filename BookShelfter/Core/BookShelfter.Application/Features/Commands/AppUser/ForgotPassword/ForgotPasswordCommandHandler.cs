using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace BookShelfter.Application.Features.Commands.AppUser.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _singInManager;
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        private readonly IEmailService _emailService;




        public ForgotPasswordCommandHandler(IAuthService authService, SignInManager<Domain.Entities.Identity.AppUser> singInManager, UserManager<Domain.Entities.Identity.AppUser> userManager, IEmailService emailService)
        {
            _authService = authService;
            _singInManager = singInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async  Task<ForgotPasswordCommandResponse> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await  _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new Exception("Yoxdur bele bir eamil nakaleniya");
            }


            var emailConfirmationToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            //var baseUrl = _configuration["Application:BaseUrl"];
            var baseUrl = "https://55fa-188-253-222-61.ngrok-free.app";

            var resetLink = $"{baseUrl}/ResetPassword.html?token={Uri.EscapeDataString(emailConfirmationToken)}&email={Uri.EscapeDataString(user.Email)}";





            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Please reset your password by clicking this link: {resetLink}");

            return new ForgotPasswordCommandResponse { Success = true, Message = "Password reset link has been sent to your email address." };
        }


    }

    }
