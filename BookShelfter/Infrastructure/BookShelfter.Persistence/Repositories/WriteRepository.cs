using BookShelfter.Application;
using BookShelfter.Domain.Entities.Common;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookShelfter.Persistence;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    protected readonly BookShelfterDbContext _context;

    public WriteRepository(BookShelfterDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();
    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
        



    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        if (datas == null || !datas.Any()) throw new ArgumentException(nameof(datas));
        await Table.AddRangeAsync(datas);
        return true;



    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public async Task<bool> RemoveAsync(string id)
    {

        //TryParse istifade et burada sonra 

        if (!int.TryParse(id, out int intId))
        {


            return false;

        }



        T model = await Table.FirstOrDefaultAsync(data => data.Id == intId);
        if (model == null)
        {
            return false;

        }
        return Remove(model);
    }

    public bool Update(T model)
    {

        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;

    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}