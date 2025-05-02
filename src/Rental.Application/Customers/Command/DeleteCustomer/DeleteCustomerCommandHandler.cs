using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;
using Rental.Domain.Interfaces;

namespace Rental.Application.Customers.Command.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger, ICustomerRepository customerRepository) : IRequestHandler<DeleteCustomerCommand>
    {
        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting customer with id : {CustomerId}", request.Id);
            var customer = await customerRepository.GetCustomersById(request.Id) ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());
            await customerRepository.DeleteAsync(customer);
        }
    }
    
}
