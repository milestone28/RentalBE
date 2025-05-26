
using Rental.Domain.Response;

namespace Rental.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<login_user_response> Login(string username, string password, string userAgent);
    }
}
