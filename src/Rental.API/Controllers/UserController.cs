using Azure;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.Users;
using Rental.Application.Users.Commands.AssignUserRole;
using Rental.Application.Users.Commands.ChangePasswordUser;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Application.Users.Commands.UnassignUserRole;
using Rental.Domain.Constants;
using System.Security.Claims;
using Tools;
using Tools.Models.Response;

namespace Rental.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController(IMediator mediator, IUserContext userContext) : Controller
    {

        [HttpPost]
        //[Authorize(Roles = AuthConstant.Roles.admin + "," + AuthConstant.Roles.owner)]
        [ProducesResponseType(typeof(base_response), 200)]
        public async Task<IActionResult> add_user([FromBody] RegisterUserCommand command)
        {
            result_response result = new result_response();
            base_response response = new base_response();

            System.Net.IPAddress remoteIpAddress1 = HttpContext.Connection.RemoteIpAddress!.MapToIPv4();
            var header = Request.Headers["Authorization"];
            var identity = HttpContext.User.Identity as ClaimsIdentity;


            if (header.ToString().StartsWith("Bearer"))
            {
                var authuser = userContext.GetCurrentUser();
                result = await mediator.Send(command);
            }
            else
            {
                result = new result_response()
                {
                    result_code = (int)ReturnCode.Token,
                    result_msg = ReturnMessage.MissingAuthHeader,
                    details = ""
                };
            }
            response = new base_response()
            {
                result_code = result.result_code,
                result_msg = result.result_msg
            };

            // var result = await mediator.Send(command);
            //return CreatedAtAction(nameof(RegisterUser), new { id = result.Id }, result);
            return Ok(response);
        }


        [HttpPatch("userChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordUser([FromForm] ChangePasswordUserCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = AuthConstant.Roles.admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = AuthConstant.Roles.admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
