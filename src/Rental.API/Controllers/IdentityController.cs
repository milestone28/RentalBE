using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.Users.Commands.AssignUserRole;
using Rental.Application.Users.Commands.ChangePasswordUser;
using Rental.Application.Users.Commands.UnassignUserRole;
using Rental.Application.Users.Commands.UpdateUserDetails;
using Rental.Domain.Constants;

namespace Rental.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : Controller
    {
        [HttpPatch("userChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordUser([FromForm] ChangePasswordUserCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = UserConstant.Roles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserConstant.Roles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
