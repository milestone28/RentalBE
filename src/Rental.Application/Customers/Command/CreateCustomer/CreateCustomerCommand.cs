

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Rental.Application.Customers.Command.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public IFormFile? ImageUpload { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
