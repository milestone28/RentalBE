

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Application.Customers.DTOs;
using Rental.Domain.Interfaces;
using Tools.Models.Request;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler(ILogger<GetAllCustomersQueryHandler> _logger, IMapper _mapper, ICustomerRepository _customerRepository) 
        : IRequestHandler<GetAllCustomersQuery, get_list_base_requestV2<CustomersDtos>>
    {
        public async Task<get_list_base_requestV2<CustomersDtos>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllCustomersQuery");
            var (customers, totalCount) = await _customerRepository.GetAllCustomers(request.searchPhrase,request.PageSize,request.PageNumber, request.SortBy, request.SortDirection, request.isDeleted);
            var customersDtos = _mapper.Map<IEnumerable<CustomersDtos>>(customers);
            var result = new get_list_base_requestV2<CustomersDtos>(customersDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
