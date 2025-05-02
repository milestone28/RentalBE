using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;

namespace Rental.Application.Users.Commands.UpdateUserDetails
{
   public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger, IUserContext _userContext, IUserStore<User> _userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);
            var dbUser = _userStore.FindByIdAsync(user!.Id, cancellationToken).Result;

            if(dbUser == null)
            {
                throw new NotFoundException(nameof(User), user!.Id);
            }
            await _userStore.UpdateAsync(dbUser, cancellationToken); 
        }
    }
}
