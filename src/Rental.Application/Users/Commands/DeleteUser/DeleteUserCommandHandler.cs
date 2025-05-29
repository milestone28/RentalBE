

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Domain.Entities.Request;
using Rental.Domain.Interfaces;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(ILogger<RegisterUserCommandHandler> _logger, IMapper _mapper, IUserContext _userContext, IUserRepository userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<DeleteUserCommand, result_response>
    {
        public async Task<result_response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting user : {Username}", request.user_id);
            var authuser = _userContext.GetCurrentUser();
            System.Net.IPAddress remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4();
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
            var user_request = _mapper.Map<user_id_request>(request);
            var result = await userRepository.DeleteUser(user_request, authuser!.Name, credentialsvalue);
            _logger.LogInformation("Delete handled successfully for user: {UserId}", request.user_id);
            return result;
        }
    }
}
