
using Tools.Models.Response;

namespace Rental.Domain.Entities.Response
{
        public class users_response : list_response
        {
            public List<user_map_response> userlist { get; set; } = default!;
        }
}
