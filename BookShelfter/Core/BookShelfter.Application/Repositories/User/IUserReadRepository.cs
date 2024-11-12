using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities.Identity;

namespace BookShelfter.Application.Repositories.User
{
    //changinin behavior especcialy for this situation
    public  interface IUserReadRepository:IUserRepository<AppUser>
    {

    }
}
