using MediatR;
using Rental.Application.Customers.DTOs;
using Tools.Models.Request;
using static Tools.Models.sort_direction;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<get_list_base_requestV2<CustomersDtos>>
    {
        public string? searchPhrase { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool isDeleted { get; set; } = false; 
    }
}
