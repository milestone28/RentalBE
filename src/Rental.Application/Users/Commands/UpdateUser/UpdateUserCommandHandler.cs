

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities.Request;
using Rental.Domain.Interfaces;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> _logger, IMapper _mapper, IUserContext _userContext, IUserRepository userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<UpdateUserCommand, result_response>
    {
        public Task<result_response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateUserCommand for user: {UserId}", request.user_id);
            var authuser = _userContext.GetCurrentUser();
            System.Net.IPAddress remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4();
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
            var _user = _mapper.Map<user_request>(request);
            var result = userRepository.UpdateUser(_user, authuser!.Name, credentialsvalue);
            _logger.LogInformation("Update handled successfully for user: {UserId}", request.user_id);
            return result;
        }
    }
}
