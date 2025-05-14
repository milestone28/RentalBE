

using AutoMapper;
using Rental.Application.Customers.DTOs;
using Rental.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Common;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandlerd(ILogger<GetAllCustomersQueryHandlerd> _logger, IMapper _mapper, ICustomerRepository _customerRepository) 
        : IRequestHandler<GetAllCustomersQuery, PageResult<CustomersDtos>>
    {
        public async Task<PageResult<CustomersDtos>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllCustomersQuery");
            var (customers, totalCount) = await _customerRepository.GetAllCustomers(request.searchPhrase,request.PageSize,request.PageNumber, request.SortBy, request.SortDirection, request.isDeleted);
            var customersDtos = _mapper.Map<IEnumerable<CustomersDtos>>(customers);
            var result = new PageResult<CustomersDtos>(customersDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
