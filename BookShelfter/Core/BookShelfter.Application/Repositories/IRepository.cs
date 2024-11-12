using BookShelfter.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application;

public interface IRepository<T>where T:BaseEntity
{
    DbSet<T> Table { get; }
    
}