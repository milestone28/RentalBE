

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Customers.Command.CreateCustomer;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;
using Rental.Domain.Interfaces;

namespace Rental.Application.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IMapper mapper, ICustomerRepository customerRepository) : IRequestHandler<UpdateCustomerCommand>
    {
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await customerRepository.GetCustomersById(request.Id) ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());
            mapper.Map(request, restaurant);
            await customerRepository.SaveChanges();
        }
    }
}
