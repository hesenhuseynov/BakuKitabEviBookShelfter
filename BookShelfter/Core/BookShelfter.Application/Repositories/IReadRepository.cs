using System.Linq.Expressions;
using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Application;

public interface IReadRepository<T>:IRepository<T>where T:BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T?> GetByIdAsync(int id, bool tracking = true);
    
    


}