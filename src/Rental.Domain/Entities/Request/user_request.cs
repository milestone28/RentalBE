

namespace Rental.Domain.Entities.Request
{
    public class user_request : user_id_request
    {
        public string first_name { get; set; } = default!;
        public string? middle_name { get; set; }
        public string last_name { get; set; } = default!;
        public string email { get; set; } = default!;
        public string mobile { get; set; } = default!;
        public DateTime? last_online { get; set; }
        public DateOnly? date_of_birth { get; set; }
        public bool ip_lock { get; set; }
        public bool status { get; set; }
        public Address? address { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_user { get; set; }
    }
}
