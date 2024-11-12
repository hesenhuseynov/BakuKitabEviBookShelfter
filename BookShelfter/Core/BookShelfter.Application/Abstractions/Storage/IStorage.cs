using System.Security.Policy;
using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace BookShelfter.Application.Abstractions.Storage;

public interface IStorage
{
    Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName,
        IFormCollection files);
    
    
    Task DeleteAsync(string pathOrContainerName, string fileName);
    List<string> GetFiles(string pathOrContainerName);
    bool HasFile(string pathOrContainerName, string fileName);

    
    
    
}