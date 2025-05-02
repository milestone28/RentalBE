

using MediatR;

namespace Rental.Application.Customers.Command.TempDeleteCustomer
{
    public class TempDeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
