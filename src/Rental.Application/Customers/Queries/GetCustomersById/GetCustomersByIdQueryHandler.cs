using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Customers.DTOs;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;
using Rental.Domain.Interfaces;

namespace Rental.Application.Customers.Queries.GetCustomersById
{
    public class GetCustomersByIdQueryHandler(ILogger<GetCustomersByIdQueryHandler> logger, IMapper mapper, ICustomerRepository customerRepository) : IRequestHandler<GetCustomersByIdQuery, CustomersDtos>
    {
        public async Task<CustomersDtos> Handle(GetCustomersByIdQuery request, CancellationToken cancellationToken)
        {   
            logger.LogInformation($"Getting customer {request.Id}");
            var customer = await customerRepository.GetCustomersById(request.Id) ?? throw new NotFoundException(nameof(Customer),request.Id.ToString());
            var customerDto = mapper.Map<CustomersDtos>(customer);
            return customerDto;
        }
    }
}
