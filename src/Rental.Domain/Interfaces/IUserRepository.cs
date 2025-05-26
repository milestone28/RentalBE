using Rental.Domain.Entities;
using Tools.Models.Response;

namespace Rental.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<result_response> AddUser(User user_request, string password_request, string user_auth);
    }
}
