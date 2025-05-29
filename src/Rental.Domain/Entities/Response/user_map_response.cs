


namespace Rental.Domain.Entities.Response
{
    public class user_map_response : base_model
    {
        public Guid id { get; set; }
        public string user_id { get; set; } = default!;
        public string first_name { get; set; } = default!;
        public string? middle_name { get; set; }
        public string last_name { get; set; } = default!;
        public string email { get; set; } = default!;
        public string mobile { get; set; } = default!;
        public DateTime? last_online { get; set; }
        public DateOnly? date_of_birth { get; set; }
        public bool ip_lock { get; set; }
        public bool status { get; set; }
        public string? city { get; set; }
        public string? street { get; set; }
        public string? postal_code { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_user { get; set; }
    }
}
