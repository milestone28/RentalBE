

using MediatR;
using Rental.Domain.Response;

namespace Rental.Application.Auth.Command.Login
{
    public class LoginCommand(string _email, string _password, string _userAgent) : IRequest<login_user_response>
    {
        public string Email { get; set; } = _email;
        public string Password { get; set; } = _password;
        public string UserAgent { get; set; } = _userAgent;
    }
}
