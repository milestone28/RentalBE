using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Helper;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;
using Rental.Domain.Interfaces;

namespace Rental.Application.Customers.Command.UploadCustomerImage
{
    public class UploadCustomerImageCommandHandler(ILogger<UploadCustomerImageCommandHandler> logger, IMapper mapper,
        ICustomerRepository customerRepository, IFileValidator fileValidator, IBlobStorageService blobStorageService) : IRequestHandler<UploadCustomerImageCommand>
    {
        public async Task Handle(UploadCustomerImageCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading image for customer with ID {CustomerId}", request.Id);
            if (request.ImageUpload == null)
            {
                logger.LogInformation("No image file provided for customer with ID {CustomerId}", request.Id);
                throw new NotFoundException("No image file provided for customer", request.Id.ToString());
            }

           var customer = await customerRepository.GetCustomersById(request.Id) ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());
            var stream = request.ImageUpload.OpenReadStream();
            fileValidator.ValidateFileUpload(request.ImageUpload);
            var imageUrl = blobStorageService.UploadFileAsync(request.Id.ToString(), request.ImageUpload).Result;
            customer.customerImageUrl = imageUrl;
            await customerRepository.SaveChanges();
        }
    }
}
