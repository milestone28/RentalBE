

using Tools.Models.Response;

namespace Rental.Domain.Response
{
    public class login_user_response : base_response
    {
        public login_user_details details { get; set; } = default!;
    }

    public class login_user_details
    {
        public string user_id { get; set; } = default!;
        public string token { get; set; } = default!;
        public DateTime? expire_in { get; set; }
        public string device_id { get; set; } = default!;
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_user { get; set; }
    }
}
