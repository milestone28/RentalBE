

using Microsoft.AspNetCore.Identity;

namespace Rental.Domain.Entities
{
    public class User : IdentityUser 
    {
        public string first_name { get; set; } = default!;
        public string last_name { get;  set; } = default!;
        public string? profile_image_url { get;  set; }
        public DateTime? last_online { get;  set; }
        public DateOnly? date_of_birth { get; set; }
        public bool is_deleted { get;  set; }
        public bool macaddress_lock { get;  set; }
        public bool status { get; set; }
        public Address? address { get; set; }
        public BaseModel? base_model { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_user { get; set; }
    }
}
