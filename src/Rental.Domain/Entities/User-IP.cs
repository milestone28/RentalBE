

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tools.Extensions;

namespace Rental.Domain.Entities
{
    [Table("setup_authorizeip")]
    public class User_IP : base_model
    {
        [Key]
        [Required]
        [LoggingPrimaryKey]
        public long id { get; set; }
        [Required]
        [MaxLength(50)]
        public string user_id { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string user_ip { get; set; } = default!;
    }
}
