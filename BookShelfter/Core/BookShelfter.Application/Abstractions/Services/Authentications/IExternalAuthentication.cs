using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;

namespace BookShelfter.Application.Abstractions.Services.Authentications
{
    public  interface IExternalAuthentication
    {
        //Task<Token>

        Task<IDataResult<DTOs.Token>> LoginGoogleAsync(string idToken);

    }
}
