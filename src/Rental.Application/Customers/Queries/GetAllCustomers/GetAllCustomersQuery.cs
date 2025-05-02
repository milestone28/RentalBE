

using MediatR;
using Rental.Application.Customers.DTOs;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomersDtos>>
    {
        public GetAllCustomersQuery()
        {
        }
    }
}
