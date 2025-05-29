
using System.ComponentModel.DataAnnotations;
using Tools.Models.Request;

namespace Rental.Domain.Entities.Request
{
    public class user_id_get_request : get_list_base_request
    {
        [MaxLength(50)]
        public Guid user_id { get; set; }
    }
}
