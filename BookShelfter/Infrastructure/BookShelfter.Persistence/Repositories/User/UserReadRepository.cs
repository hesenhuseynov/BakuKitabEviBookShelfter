using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.User;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.User
{
    public  class UserReadRepository:IUserReadRepository
    {
        private readonly BookShelfterDbContext _context;


        public UserReadRepository(BookShelfterDbContext context)
        {
            _context = context;
         
        }

        public DbSet<AppUser> Table => _context.Set<AppUser>();


        public IQueryable<AppUser> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }


        public IQueryable<AppUser> GetWhere(Expression<Func<AppUser, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<AppUser?> GetSingleAsync(Expression<Func<AppUser, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<AppUser?> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
