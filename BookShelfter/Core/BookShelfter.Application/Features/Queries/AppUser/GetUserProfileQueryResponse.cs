using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.AppUser
{
    public  class GetUserProfileQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string NameSurName { get; set; }
    }
}
