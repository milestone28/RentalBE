

using MediatR;
using Rental.Application.Customers.DTOs;

namespace Rental.Application.Customers.Queries.GetCustomersById
{
    public class GetCustomersByIdQuery(Guid id) : IRequest<CustomersDtos>
    {
        public Guid Id { get; set; } = id;
    }
}
