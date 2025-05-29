using Tools.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rental.Domain.Entities
{
    [Table("activitylogs")]
    public class Activitylogs
    {
        [Key]
        [Required]
        [LoggingPrimaryKey]
        public int id { get;  set; } = default!;
       
        public string? user_id { get; set; }
        [Required]
        public DateTime activity_datetime { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string activity_ticktime { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string ip_address { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string action { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        public string httpverb { get; set; } = default!;
        [Required]
        public string activity_details { get; set; } = default!;
        [Required]
        public string activity_response { get; set; } = default!;
        public string activity_description { get; set; } = default!;
        public string? add_details { get; set; }
        public string? update_details { get; set; }
        public string? delete_details { get; set; }
        public string? activity_details1 { get; set; }
        public string? activity_details2 { get; set; }
        public string? activity_details3 { get; set; }
        public string? activity_details4 { get; set; }

    }
}
