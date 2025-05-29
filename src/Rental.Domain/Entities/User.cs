

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tools.Extensions;

namespace Rental.Domain.Entities
{
    [Table("setup_users")]
    public class User : base_model
    {
        [Key]
        [Required]
        [LoggingPrimaryKey]
        public Guid id { get; set; }
        [MaxLength(100)]
        public string user_id { get; set; } = default!;
        [MaxLength(255)]
        public string first_name { get; set; } = default!;
        [MaxLength(255)]
        public string? middle_name { get; set; }
        [MaxLength(255)]
        public string last_name { get;  set; } = default!;
        [MaxLength(255)]
        public string salt { get; set; } = default!;
        [MaxLength(100)]
        public string password { get; set; } = default!;
        [MaxLength(100)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; } = default!;
        [MaxLength(100)]
        public string mobile { get; set; } = default!;
        public DateTime? last_online { get;  set; }
        public DateOnly? date_of_birth { get; set; }
        public bool ip_lock { get; set; }
        public bool status { get; set; }
        public Address? address { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_user { get; set; }
    }
}
