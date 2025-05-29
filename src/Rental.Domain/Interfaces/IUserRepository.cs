using Rental.Domain.Entities;
using Rental.Domain.Entities.Request;
using Rental.Domain.Entities.Response;
using Tools.Models.Response;

namespace Rental.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<result_response> AddUser(User user_request, string password_request, string user_auth, string credentials_value);
        Task<users_response> GetUser(users_get_request request, string user_auth, string credentials_value);
        Task<result_response> UpdateUser(user_request request, string user_auth, string credentials_value);
        Task<result_response> DeleteUser(user_id_request request, string user_auth, string credentials_value);
    }
}
