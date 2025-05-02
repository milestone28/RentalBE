

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Configuration;

namespace Rental.Infrastructure.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> options) : IBlobStorageService
    {
        private readonly BlobStorageSettings _settings = options.Value;
        public async Task<string> UploadFileAsync(string _fileName, IFormFile _file)
        {
            var containerClient = new BlobContainerClient(_settings.ConnectionString, _settings.ContainerName);
            await containerClient.CreateIfNotExistsAsync();

            var contentType = _file.ContentType.ToLowerInvariant();

            var blobClient = containerClient.GetBlobClient((_fileName + System.IO.Path.GetExtension(_file.FileName).ToLower()));

            var httpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            using (var stream = _file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, httpHeaders);
            }
            return blobClient.Uri.ToString();
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var containerClient = new BlobContainerClient(_settings.ConnectionString, _settings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public string GetBlobSasUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);
            var blobName = uri.Segments.Last();
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _settings.ContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
                BlobName = blobName
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var blobServiceClient = new BlobServiceClient(_settings.ConnectionString);
            var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(_settings.AccountName, _settings.AccountKey)).ToString();
            return $"{blobUrl}?{sasToken}";
        }
    }
}
