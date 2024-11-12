using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Repositories.User
{
    public  interface IUserRepository<T> where T:class
    {
        DbSet<T> Table { get; }
    }
}
