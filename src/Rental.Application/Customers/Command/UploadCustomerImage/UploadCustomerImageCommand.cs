

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Rental.Application.Customers.Command.UploadCustomerImage
{
    public class UploadCustomerImageCommand : IRequest
    {
        public Guid Id { get; set; }
        public IFormFile? ImageUpload { get; set; }
    }
}
