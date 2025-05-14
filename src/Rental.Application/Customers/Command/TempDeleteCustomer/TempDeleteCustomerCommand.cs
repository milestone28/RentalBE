

using MediatR;

namespace Rental.Application.Customers.Command.TempDeleteCustomer
{
    public class TempDeleteCustomerCommand(Guid guid) : IRequest
    {
        public Guid Id { get; set; } = guid;
    }
}
