

using AutoMapper;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Helper;

namespace Rental.Application.Customers.Command.CreateCustomer
{
    public class CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IMapper mapper, 
        ICustomerRepository customerRepository, IFileValidator fileValidator, IBlobStorageService blobStorageService) : IRequestHandler<CreateCustomerCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Customer with details {CustomerDetails}", request);

            var customer = mapper.Map<Customer>(request);
            if (request.ImageUpload != null)
            {
                fileValidator.ValidateFileUpload(request.ImageUpload);
                var imageUrl = blobStorageService.UploadFileAsync(request.Name, request.ImageUpload);
                customer.customerImageUrl = imageUrl.Result;
            }
            
            var customerId = await customerRepository.CreateCustomer(customer);
            return customerId;
        }
    }
}
