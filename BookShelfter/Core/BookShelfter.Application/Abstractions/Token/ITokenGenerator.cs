using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Abstractions.Token
{
    public  interface ITokenGenerator
    {
        public string GenerateJwtToken((string tokenId, string userName, IList<string> roles) userDetails);

    }
}
