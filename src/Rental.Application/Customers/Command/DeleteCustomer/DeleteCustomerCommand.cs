

using MediatR;

namespace Rental.Application.Customers.Command.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
