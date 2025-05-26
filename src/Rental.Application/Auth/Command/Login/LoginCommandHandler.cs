

using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Domain.Interfaces;
using Rental.Domain.Response;

namespace Rental.Application.Auth.Command.Login
{
    public class LoginCommandHandler(ILogger<LoginCommandHandler> _logger, IAuthRepository _authRepository) : IRequestHandler<LoginCommand, login_user_response>
    {
        public Task<login_user_response> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Logging In.");
            var response = _authRepository.Login(request.Email, request.Password, request.UserAgent);
            return response;
        }
    }
}
