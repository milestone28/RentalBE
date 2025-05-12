

using MediatR;
using Rental.Application.Common;
using Rental.Application.Customers.DTOs;
using Rental.Domain.Constant;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<PageResult<CustomersDtos>>
    {
        public string? searchPhrase { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
    }
}
