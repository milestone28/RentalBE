
using Microsoft.AspNetCore.Http;

namespace Rental.Domain.Interfaces
{
   public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(string fileName, IFormFile file);
        Task DeleteFileAsync(string fileName);
        string GetBlobSasUrl(string blobUrl);
    }
}
