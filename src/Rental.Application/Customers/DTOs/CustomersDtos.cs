using Rental.Domain.Entities.Response;
using Tools.Models.Response;

namespace Rental.Application.Customers.DTOs
{
    public class CustomersDtos : base_response
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public string? customerImageUrl { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
