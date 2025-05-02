

using AutoMapper;
using Rental.Application.Customers.DTOs;
using Rental.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandlerd(ILogger<GetAllCustomersQueryHandlerd> _logger, IMapper _mapper, ICustomerRepository _customerRepository) 
        : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomersDtos>>
    {
        public async Task<IEnumerable<CustomersDtos>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllCustomersQuery");
            var customers = await _customerRepository.GetAllCustomers();
           var customersDtos = _mapper.Map<IEnumerable<CustomersDtos>>(customers);
            return customersDtos;
        }
    }
}
