

namespace Rental.Domain.Entities.Request
{
    public class users_get_request : user_id_get_request
    {
        public string? is_admin { get; set; }
        public string? is_owner { get; set; }
        public string? is_user { get; set; }
    }
}
