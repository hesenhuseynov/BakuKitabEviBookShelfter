using BookShelfter.Application.Abstractions.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google;

namespace BookShelfter.Infrastructure.Services.Storage.Google
{
    public class GoogleCloudStorage : IFileStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            _bucketName = configuration["GoogleCloud:BucketName"];
            var credentialPath = Path.Combine(AppContext.BaseDirectory, configuration["GoogleCloud:CredentialPath"]);


            Console.WriteLine(credentialPath);

            GoogleCredential credential;
            using (var jsonStream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                credential = GoogleCredential.FromStream(jsonStream);


            }

            _storageClient = StorageClient.Create(credential);



        }

        public async  Task<bool> DeleteFileAsync(string fileUrl)
        {

            try
            {

                var uri = new Uri(fileUrl);

                var objectName = Path.GetFileName(uri.LocalPath);
                await _storageClient.DeleteObjectAsync(_bucketName, objectName);
                return true;

            }
            catch (Exception)
            {
                return false;
            }




        }

        public async  Task<object> GetFileMetaDataAsync(string fileName)
        {
            try
            {

                var storageObject = await _storageClient.GetObjectAsync(_bucketName, fileName);
                return storageObject;

            }
            catch (Exception e )
            {

                Console.WriteLine(e.Message);
                throw;
            }


            
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var objectName = Guid.NewGuid() + Path.GetExtension(fileName);



            //await _storageClient.UploadObjectAsync(_bucketName, objectName, null, fileStream);

            var obj = await _storageClient.UploadObjectAsync(
                bucket: _bucketName,
                objectName: objectName,
                contentType: contentType,
                source: fileStream
               
            );


            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";



        }
    }
}
