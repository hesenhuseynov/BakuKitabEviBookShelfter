using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services.Authentications;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;

namespace BookShelfter.Application.Abstractions.Services
{
    public interface IAuthService:IInternalAuthenticaiton,IExternalAuthentication
    {
        //Task<DTOs.Token> RegisterAsync(string email, string password, int accessTokenLifeTime); 
        Task<IResult> RegisterAsync(RegisterDto registerDto, int accessTokenLifeTime);
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetToken(string resetToken, string userId);
        Task<IResult> ConfirmEmailAsyncAndCreateToken(string token, string email, int accessTokenLifeTime);




    }
}
