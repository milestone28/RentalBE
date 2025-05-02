

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Domain.Exceptions;

namespace Rental.Application.Helper
{
    public interface IFileValidator
    {
        bool ValidateFileUpload(IFormFile _file);
    }
    internal class FileValidator(ILogger<FileValidator> logger) : IFileValidator
    {
        public bool ValidateFileUpload(IFormFile _file)
        {
            logger.LogInformation("Validating file upload for {FileName}", _file.FileName);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
            var maxFileSize = 10 * 1024 * 1024; // 10 MB


            if (!allowedExtensions.Contains(System.IO.Path.GetExtension(_file.FileName).ToLower()))
            {
                logger.LogInformation("Invalid file type. Only .jpg, .jpeg, and .png are allowed.");
                throw new BadRequestException("Invalid file type. Only .jpg, .jpeg, and .png are allowed.");

            }

            if (_file.Length > maxFileSize)
            {
                logger.LogInformation("File size exceeds the limit of 10 MB.");
                throw new BadRequestException("File size exceeds the limit of 10 MB.");
            }
            if (!allowedMimeTypes.Contains(_file.ContentType.ToLowerInvariant()))
            {
                throw new BadRequestException($"MIME type '{_file.ContentType}' is not supported.");
            }

            return true;
        }
    }

   
}
