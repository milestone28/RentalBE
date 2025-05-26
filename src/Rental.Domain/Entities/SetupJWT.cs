

using System.ComponentModel.DataAnnotations.Schema;

namespace Rental.Domain.Entities
{
    [Table("setup_jwt")]
    public class SetupJWT
    {
        public int id { get; set; }
        public string user_id { get; set; } = default!;
        public string user_token { get; set; } = default!;
        public DateTime expire_in { get; set; } = default!;
        public string device_id { get; set; } = default!;
    }
}
