using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BookShelfter.Application.Abstractions.Storage;

public interface IStorageService:IStorage
{
    public string StorageName { get; }
      
    

    
}