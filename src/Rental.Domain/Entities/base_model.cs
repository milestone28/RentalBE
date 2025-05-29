

using System.ComponentModel.DataAnnotations;

namespace Rental.Domain.Entities
{
    public class base_model : ICloneable
    {
        [MaxLength(255)]
        public string? extra1 { get; set; }
        [MaxLength(255)]
        public string? extra2 { get; set; }
        [MaxLength(255)]
        public string? extra3 { get; set; }
        [MaxLength(255)]
        public string? extra4 { get; set; }
        public string? notes1 { get; set; }
        public string? notes2 { get; set; }
        public string? notes3 { get; set; }
        public string? notes4 { get; set; }

        [Required]
        public DateTime created_date { get; set; }

        [Required]
        [MaxLength(255)]
        public string? created_by { get; set; }
        [Required]
        public DateTime updated_date { get; set; }

        [Required]
        [MaxLength(255)]
        public string? updated_by { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
