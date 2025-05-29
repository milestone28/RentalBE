using Azure;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.Users;
using Rental.Application.Users.Commands.DeleteUser;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Application.Users.Commands.UpdateUser;
using Rental.Application.Users.Querries;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Domain.Entities.Request;
using Rental.Domain.Entities.Response;
using Rental.Domain.Interfaces;
using System.Security.Claims;
using Tools;
using Tools.Models.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rental.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController(IMediator mediator, IUserContext userContext, IActivityLogRepository _activityLogRepository) : Controller
    {

        [HttpPost]
        [Authorize(Roles = AuthConstant.Roles.admin + "," + AuthConstant.Roles.owner)]
        [ProducesResponseType(typeof(base_response), 200)]
        public async Task<IActionResult> add_user([FromBody] RegisterUserCommand command)
        {
            result_response result = new result_response();
            base_response response = new base_response();
            Activitylogs activitylogs = new Activitylogs();
            string authuser = "";
            System.Net.IPAddress remoteIpAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4();
            var header = Request.Headers["Authorization"];
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (header.ToString().StartsWith("Bearer"))
            {
                var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
                authuser = userContext.GetCurrentUser()!.Name;
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

            var logdatetime = DateTime.Now;
            var activityresponse = response.toJSON();

            activitylogs = new Activitylogs()
            {
                user_id = authuser,
                activity_datetime = logdatetime,
                activity_ticktime = logdatetime.Ticks.ToString("x"),
                ip_address = remoteIpAddress.ToString(),
                action = "add_user",
                httpverb = "POST",
                activity_details = command.toJSON(),
                activity_response = activityresponse,
                activity_description = "User: " + authuser + " add new user " + command.user_id + " " + logdatetime.ToString(),
                add_details = result.details,
                update_details = "",
                delete_details = "",
                activity_details1 = "",
                activity_details2 = "",
                activity_details3 = "",
                activity_details4 = "",
            };
            await _activityLogRepository.LogActivity(activitylogs);

            return Ok(response);
        }



        [HttpGet]
        [Authorize(Roles = AuthConstant.Roles.admin + "," + AuthConstant.Roles.owner)]
        [ProducesResponseType(typeof(users_response), 200)]
        public async Task<IActionResult> get_user([FromQuery] GetUserQueries query)
        {
            Activitylogs activitylogs = new Activitylogs();
            users_response response = new users_response();

            string authuser = "";

            System.Net.IPAddress remoteIpAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4();
            var header = Request.Headers["Authorization"];
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (header.ToString().StartsWith("Bearer"))
            {
                var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
                authuser = userContext.GetCurrentUser()!.Name;
                response = await mediator.Send(query);
            }
            else
            {
                response = new users_response()
                {
                    result_code = (int)ReturnCode.Token,
                    result_msg = ReturnMessage.MissingAuthHeader,
                    no_of_records = 0,
                    page_no = 0,
                    page_size = 0,
                    total_page_count = 0,
                    userlist = new List<user_map_response>()
                };
            }

            var logdatetime = DateTime.Now;
            var activityresponse = response.toJSON();

            activitylogs = new Activitylogs()
            {
                user_id = authuser,
                activity_datetime = logdatetime,
                activity_ticktime = logdatetime.Ticks.ToString("x"),
                ip_address = remoteIpAddress.ToString(),
                action = "get_user",
                httpverb = "GET",
                activity_details = query.toJSON(),
                activity_response = "",
                activity_description = "User: " + authuser + " fetch user list " + logdatetime.ToString(),
                add_details = "",
                update_details = "",
                delete_details = "",
                activity_details1 = "",
                activity_details2 = "",
                activity_details3 = "",
                activity_details4 = "",
            };
            await _activityLogRepository.LogActivity(activitylogs);

            return Ok(response);
        }


        [HttpPut]
        [Authorize(Roles = AuthConstant.Roles.admin + "," + AuthConstant.Roles.owner)]
        [ProducesResponseType(typeof(base_response), 200)]
        public async Task<IActionResult> update_user([FromBody] UpdateUserCommand command)
        {
            result_response result = new result_response();
            base_response response = new base_response();
            Activitylogs activitylogs = new Activitylogs();
            string authuser = "";
            System.Net.IPAddress remoteIpAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4();
            var header = Request.Headers["Authorization"];
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (header.ToString().StartsWith("Bearer"))
            {
                var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
                authuser = userContext.GetCurrentUser()!.Name;
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

            var logdatetime = DateTime.Now;
            var activityresponse = response.toJSON();

            activitylogs = new Activitylogs()
            {
                user_id = authuser,
                activity_datetime = logdatetime,
                activity_ticktime = logdatetime.Ticks.ToString("x"),
                ip_address = remoteIpAddress.ToString(),
                action = "update_user",
                httpverb = "PUT",
                activity_details = command.toJSON(),
                activity_response = activityresponse,
                activity_description = "User: " + authuser + " update user " + command.user_id + " " + logdatetime.ToString(),
                add_details = "",
                update_details = result.details,
                delete_details = "",
                activity_details1 = "",
                activity_details2 = "",
                activity_details3 = "",
                activity_details4 = "",
            };
            await _activityLogRepository.LogActivity(activitylogs);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = AuthConstant.Roles.admin + "," + AuthConstant.Roles.owner)]
        [ProducesResponseType(typeof(base_response), 200)]
        public async Task<IActionResult> delete_user([FromBody] DeleteUserCommand command)
        {
            result_response result = new result_response();
            base_response response = new base_response();
            Activitylogs activitylogs = new Activitylogs();
            string authuser = "";
            System.Net.IPAddress remoteIpAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4();
            var header = Request.Headers["Authorization"];
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (header.ToString().StartsWith("Bearer"))
            {
                var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
                authuser = userContext.GetCurrentUser()!.Name;
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

            var logdatetime = DateTime.Now;
            var activityresponse = response.toJSON();

            activitylogs = new Activitylogs()
            {
                user_id = authuser,
                activity_datetime = logdatetime,
                activity_ticktime = logdatetime.Ticks.ToString("x"),
                ip_address = remoteIpAddress.ToString(),
                action = "delete_user",
                httpverb = "DELETE",
                activity_details = command.toJSON(),
                activity_response = activityresponse,
                activity_description = "User: " + authuser + " delete user " + command.user_id + " " + logdatetime.ToString(),
                add_details = "",
                update_details = "",
                delete_details = result.details,
                activity_details1 = "",
                activity_details2 = "",
                activity_details3 = "",
                activity_details4 = "",
            };
            await _activityLogRepository.LogActivity(activitylogs);

            return Ok(response);
        }
    }
}
