

using AutoMapper;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Rental.Application.Customers.Command.CreateCustomer
{
    public class CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IMapper mapper, 
        ICustomerRepository customerRepository) : IRequestHandler<CreateCustomerCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Customer with details {CustomerDetails}", request);
            var customer = mapper.Map<Customer>(request);
            var customerId = await customerRepository.CreateCustomer(customer);
            return customerId;
        }
    }
}
