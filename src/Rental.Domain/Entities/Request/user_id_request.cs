

using System.ComponentModel.DataAnnotations;

namespace Rental.Domain.Entities.Request
{
    public class user_id_request
    {
        [Required]
        [MaxLength(50)]
        public string user_id { get; set; } = default!;
    }
}
