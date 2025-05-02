

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rental.Application.Users.Commands.UpdateUserDetails;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;

namespace Rental.Application.Users.Commands.ChangePasswordUser
{
    public class ChangePasswordUserCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger, IUserContext _userContext, UserManager<User> _userManager) : IRequestHandler<ChangePasswordUserCommand>
    {
        public async Task Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Changing password for user: {@Request}", request);
            var curentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByEmailAsync(curentUser.Email) ?? throw new NotFoundException(nameof(User), curentUser.Email);
            var test =  await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!test.Succeeded)
            {
                foreach(var changePassword in test.Errors)
                {
                    _logger.LogError("Error changing password: {Error}", changePassword.Description);
                    throw new BadRequestException(changePassword.Description);
                }
            }
        }
    }
}
