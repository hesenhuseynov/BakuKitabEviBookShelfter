using BookShelfter.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Abstractions.Token
{
    public  interface ITokenService
    {
        DTOs.Token CreateAccessToken(int minute, AppUser user,IList<string> roles);

        string CreateRefreshToken();
    }
}
