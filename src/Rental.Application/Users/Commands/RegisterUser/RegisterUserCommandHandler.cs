

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> _logger, IMapper _mapper, IUserContext _userContext, IUserRepository userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<RegisterUserCommand, result_response>
    {
        public async Task<result_response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registering user : {Username}", request.user_id);
            var authuser = _userContext.GetCurrentUser();
            System.Net.IPAddress remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4();
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
            var user_request = _mapper.Map<User>(request);
            var result = await userRepository.AddUser(user_request, request.password, authuser!.Name, credentialsvalue);
            _logger.LogInformation("Register handled successfully for user: {UserId}", request.user_id);
            return result;
        }
    }
}
