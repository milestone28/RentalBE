﻿
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Exceptions;

namespace Rental.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning user role: {@Request}", request);
            var user = await _userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundException(nameof(User), request.UserEmail);
            var role = await _roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await _userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
