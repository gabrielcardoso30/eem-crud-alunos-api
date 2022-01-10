using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Helpers
{
    public class BlobStorage : IBlobStorage
    {

        private readonly AppSettings _appSettings;

        public BlobStorage(
            IOptions<AppSettings> appSettings
        )
        {
            _appSettings = appSettings.Value;
        }

        public async Task<BlobContainerClient> CreateBlobContainerAsync(
            string containerName
        )
        {

            //BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            var containerClient = new BlobContainerClient(_appSettings.StorageConnectionString, containerName);
            containerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
            return containerClient;

        }

        public BlobContainerClient CheckIfExistsBlobContainer(
            string containerName
        )
        {

            BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            bool isExist = containerClient.Exists();

            if (isExist) return containerClient;
            else return null;

        }

        public async Task<string> UploadFileAsync(
            string contentType,
            Stream fileStream,
            string fileContainer,
            BlobContainerClient containerClient
        )
        {

            var fileFormat = contentType.Split("/");
            var fullNameFile = String.Concat(Guid.NewGuid().ToString(), ".", fileFormat[1]);
            BlobClient blobClient = containerClient.GetBlobClient(fullNameFile);

            await blobClient.UploadAsync(fileStream, true);

            return String.Concat(blobClient.Uri.AbsoluteUri);

        }

        public async Task DeleteBlobContainerAsync(
            string containerName
        )
        {

            BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.DeleteIfExistsAsync();

        }

        public string GetUrlFromStorage(
            string name
        )
        {
            return String.Concat(_appSettings.StorageUrlBlobFiles, name);
        }


        /*
                public async Task<string> UploadFileAsync(eStorageContainer eStorageContainer, IFormFile file, string blobReference)
                {
                    var arquivo = new byte[file.Length];
                    using (var imageStream = file.OpenReadStream())
                    {
                        for (var i = 0; i < file.Length; i++)
                        {
                            arquivo[i] = (byte)imageStream.ReadByte();
                        }
                    }

                    var container = await GetContainer(eStorageContainer);
                    var criado = await container.CreateIfNotExistsAsync();

                    if (criado)
                    {   // se o blob acaba de ser criado atribui a permissão a ele
                        SetPublicContainerPermissions(container);
                    }

                    var fileBlob = container.GetBlockBlobReference(blobReference);

                    using (var ms = new MemoryStream(arquivo))
                        await fileBlob.UploadFromStreamAsync(ms);

                    fileBlob.Properties.ContentType = file.ContentType;
                    await fileBlob.SetPropertiesAsync();

                    return fileBlob.Uri.AbsoluteUri;
                }

                public static async Task<string> UploadVideoAsync(byte[] arquivo, string extencao)
                {
                    var container = await GetContainer(eStorageContainer.Post);
                    var _criado = await container.CreateIfNotExistsAsync();

                    if (_criado)
                    {   // se o blob acaba de ser criado atribui a permissão a ele
                        SetPublicContainerPermissions(container);
                    }

                    var name = Guid.NewGuid().ToString();
                    name = name + "." + extencao.TrimStart('.');
                    var fileBlob = container.GetBlockBlobReference(name);

                    using (var ms = new MemoryStream(arquivo))
                        await fileBlob.UploadFromStreamAsync(ms);

                    fileBlob.Properties.ContentType = FileHelper.GetMimeType(extencao);
                    await fileBlob.SetPropertiesAsync();

                    return fileBlob.Uri.AbsoluteUri;
                }

                public async Task<bool> DeleteFileAsync(eStorageContainer eStorageContainer, string nome)
                {
                    var container = await GetContainer(eStorageContainer);
                    var blob = container.GetBlobReference(nome);
                    return await blob.DeleteIfExistsAsync();
                }

                private async Task<CloudBlobContainer> GetContainer(eStorageContainer eStorageContainer)
                {
                    var account = CloudStorageAccount.Parse(_appSettings.StorageConnectionString);
                    var client = account.CreateCloudBlobClient();
                    return client.GetContainerReference(eStorageContainer.ToString().ToLower());
                }

                public enum eStorageContainer
                {
                    Veiculo = 1,
                    Empresa = 2,
                    Motorista = 3
                }

                public static async void SetPublicContainerPermissions(CloudBlobContainer container)
                {
                    var permissions = await container.GetPermissionsAsync();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    await container.SetPermissionsAsync(permissions);
                }
                */

        //public static async Task<MemoryStream> DownloadFileAsync(string urlConteudo)
        //{
        //    MemoryStream _arquivo = null;

        //    try
        //    {
        //        var _url = urlConteudo.Split('/');

        //        if (_url == null)
        //            return _arquivo;

        //        string _guid = _url.LastOrDefault();

        //        var _container = await GetContainer(eStorageContainer.Post);

        //        CloudBlockBlob blockBlob = _container.GetBlockBlobReference(_guid);

        //        _arquivo = new MemoryStream();

        //        await blockBlob.DownloadToStreamAsync(_arquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return _arquivo;
        //}
        public Task<string> UploadFileAsync(string eStorageContainer, IFormFile file, string blobReference)
        {
            //TODO Fazer
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteFileAsync(string eStorageContainer, string nome)
        {
            //TODO Fazer
            throw new System.NotImplementedException();
        }
    }
}
