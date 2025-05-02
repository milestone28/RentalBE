

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;
using Rental.Domain.Interfaces;

namespace Rental.Application.Customers.Command.TempDeleteCustomer
{
    public class TempDeleteCustomerCommandHandler(ILogger<TempDeleteCustomerCommandHandler> logger, IMapper mapper, ICustomerRepository customerRepository) : IRequestHandler<TempDeleteCustomerCommand>
    {
        public async Task Handle(TempDeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Temporary deleting customer with id : {Id} with {@Customer}", request.Id, request);
            var customer = await customerRepository.GetCustomersById(request.Id) ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());
            mapper.Map(request, customer);
            await customerRepository.SaveChanges();
        }
    }
}
