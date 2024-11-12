using BookShelfter.Application.Abstractions.Storage;
using BookShelfter.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Infrastructure.Services.Storage.Local
{
    internal class LocalStorage :  ILocalStorage
    {
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormCollection files)
        {
            throw new NotImplementedException();
        }

        bool IStorage.HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
