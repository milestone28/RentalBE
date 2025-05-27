using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.Auth.Command.Login;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Domain.Response;
using System.Text;
using Tools;

namespace Rental.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController(IMediator mediatR, IActivityLogRepository _activityLogRepository) : Controller
    {
        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(login_user_response), 200)]
        public async Task<IActionResult> login()
        {
            login_user_response response = new login_user_response();
            Activitylogs activitylogs = new Activitylogs();
            System.Net.IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4();

            var header = Request.Headers["Authorization"];
            var userAgent = Request.Headers["User-Agent"].ToString();

            if (header.ToString().StartsWith("Basic"))
            {
                var credentialsValue = header.ToString().Substring("Basic ".Length).Trim();
                var hUsernameAndPass = Encoding.UTF8.GetString(Convert.FromBase64String(credentialsValue));
                var userAndPass = hUsernameAndPass.Split(":");
                response = await mediatR.Send(new LoginCommand(userAndPass[0], userAndPass[1], userAgent));
            }
            else
            {
                response = new login_user_response()
                {
                    result_code = (int)ReturnCode.Token,
                    result_msg = ReturnMessage.MissingAuthHeader,
                    details = new login_user_details()
                    {
                        user_id = "",
                        token = "",
                        expire_in = null,
                        device_id = "",
                        is_admin = false,
                        is_owner = false,
                        is_user = false
                    }
                };
            }

            var logdatetime = DateTime.Now;
            string tokenString = response.details.token;
            var activityresponse = response.toJSON();
            response.details.token = tokenString;
            activitylogs = new Activitylogs()
            {
                id = 0,
                user_id = response.details.user_id,
                activity_datetime = logdatetime,
                activity_ticktime = logdatetime.Ticks.ToString("x"),
                ip_address = remoteIpAddress.ToString(),
                action = "login",
                httpverb = "POST",
                activity_details = "",
                activity_response = activityresponse,
                activity_description = "User: " + response.details.user_id + " logged in " + logdatetime.ToString(),
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
   
    
    
    }
}
