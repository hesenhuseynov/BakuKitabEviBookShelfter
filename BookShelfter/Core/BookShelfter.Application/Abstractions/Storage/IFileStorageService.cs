using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Abstractions.Storage
{
    public interface IFileStorageService
    {
        Task<string>UploadFileAsync(Stream fileStream,string fileName,string contentType);
        Task<bool> DeleteFileAsync(string fileUrl);
        Task<object> GetFileMetaDataAsync(string fileName);

        
        




    }
}
