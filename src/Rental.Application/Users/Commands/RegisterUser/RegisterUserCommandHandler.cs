

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> _logger, IMapper _mapper, IUserContext _userContext, IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, result_response>
    {
        public Task<result_response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registering user with email: {Email}", request.email);
            var authuser = _userContext.GetCurrentUser();
            var user = _mapper.Map<User>(request);
            var result = userRepository.AddUser(user, request.password, authuser!.Roles.FirstOrDefault() ?? "");
            return result;
        }
    }
}
