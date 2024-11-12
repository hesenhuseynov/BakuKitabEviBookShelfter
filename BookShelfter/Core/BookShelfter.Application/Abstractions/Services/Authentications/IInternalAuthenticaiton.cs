using BookShelfter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Common;

namespace BookShelfter.Application.Abstractions.Services.Authentications
{
    public  interface IInternalAuthenticaiton
    {
        Task<IResult> LoginUserAsync(LoginDto loginDto, int accessTokenLifeTime);

        Task<DataResult<DTOs.Token>> RefreshTokenLoginAsync(string refreshToken);





    }
}
