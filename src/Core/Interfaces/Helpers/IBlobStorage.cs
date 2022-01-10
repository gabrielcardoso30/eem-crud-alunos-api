using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Helpers
{

    public interface IBlobStorage
    {
        
        //Task<string> UploadFileAsync(string eStorageContainer, IFormFile file, string blobReference);
        //Task<bool> DeleteFileAsync(string eStorageContainer, string nome);

        Task<string> UploadFileAsync(
            string contentType,
            Stream fileStream,
            string fileContainer,
            BlobContainerClient containerClient
        );

        Task<BlobContainerClient> CreateBlobContainerAsync(string containerName);

        BlobContainerClient CheckIfExistsBlobContainer(string containerName);

        string GetUrlFromStorage(string name);

        Task DeleteBlobContainerAsync(
            string containerName
        );

    }

}
