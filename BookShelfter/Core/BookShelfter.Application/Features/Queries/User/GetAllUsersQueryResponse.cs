using BookShelfter.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.User
{
    public  class GetAllUsersQueryResponse
    {
        public bool  Sucess { get; set; }
        public string Message { get; set; }

        public List<UserDto> Users { get; set; }

    }
}
