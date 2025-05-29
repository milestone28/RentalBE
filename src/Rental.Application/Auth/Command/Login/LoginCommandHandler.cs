

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Domain.Interfaces;
using Rental.Domain.Response;

namespace Rental.Application.Auth.Command.Login
{
    public class LoginCommandHandler(ILogger<LoginCommandHandler> _logger, IAuthRepository _authRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<LoginCommand, login_user_response>
    {
        public Task<login_user_response> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            System.Net.IPAddress remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4();
            _logger.LogInformation("Logging In.");
            var response = _authRepository.Login(request.Email, request.Password, request.UserAgent, remoteIpAddress.ToString());
            return response;
        }
    }
}
