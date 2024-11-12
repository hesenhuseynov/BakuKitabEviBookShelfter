using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.User;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.User
{
    public class UserWriteRepository:IUserWriteRepository
    {

        private readonly BookShelfterDbContext _context;

        public UserWriteRepository(BookShelfterDbContext context)
        {
            _context = context;
        }

        public DbSet<AppUser> Table => _context.Set<AppUser>();


        public async Task<bool> AddAsync(AppUser entity)
        {
            await Table.AddAsync(entity);
            return true;
        }

        public bool Remove(AppUser entity)
        {
            Table.Remove(entity);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var entity = await Table.FirstOrDefaultAsync(user => user.Id == id);
            if (entity == null) return false;
            Table.Remove(entity);
            return true;
        }

        public bool Update(AppUser entity)
        {
            Table.Update(entity);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
