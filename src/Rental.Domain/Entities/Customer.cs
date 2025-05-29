


using System.ComponentModel.DataAnnotations.Schema;

namespace Rental.Domain.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? customerImageUrl { get; set; }
        public Address? Address { get; set; } = default!;
    }
}
