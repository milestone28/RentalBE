

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rental.Application.Users;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using System.Text.Json;
using Tools;
using Tools.Models.Response;

namespace Rental.Infrastructure.Repositories
{
    internal class UserRepository(UserManager<User> userManager, ILogger<UserRepository> _logger) : IUserRepository
    {
        public async Task<result_response> AddUser(User user_request, string password_request, string user_auth)
        {
            result_response result = new result_response();

            try
            {
                // check duplicate UserID
                //var search_user = await _userManager.FindByEmailAsync(user_request.Email!);
                //if(search_user != null)
                //{
                //    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserIdExist);
                //    return result;
                //}

                var identityResult = await userManager.CreateAsync(user_request, password_request);
                if (!identityResult.Succeeded)
                {
                    // Check for duplicate user error
                    if (identityResult.Errors.Any(e => e.Description.Contains("duplicate")))
                    {
                        result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserIdExist);
                    }
                    else
                    {
                        // Collect all error messages and return them
                        var errorMessages = identityResult.Errors.Select(e => e.Description).ToList();
                        string jsonResult = JsonSerializer.Serialize(errorMessages);
                        result = CreateResponse<result_response>(1, (int)ReturnCode.Error, jsonResult);
                    }
                }

                switch (user_auth)
                {
                    case AuthConstant.Roles.admin:
                        identityResult = await userManager.AddToRoleAsync(user_request, AuthConstant.Roles.owner);
                        break;
                    case AuthConstant.Roles.owner:
                        identityResult = await userManager.AddToRoleAsync(user_request, AuthConstant.Roles.renter);
                        break;
                    default:
                        identityResult = await userManager.AddToRoleAsync(user_request, AuthConstant.Roles.renter);
                        break;
                }

                result = CreateResponse<result_response>(1, (int)ReturnCode.Success, ReturnMessage.Created, user_request.Id);
            }

            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while creating a user.");
                result = CreateResponse<result_response>(1, (int)ReturnCode.Exception, ReturnMessage.ErrorException, ex.Message);
            }

            return result;
        }

        private T CreateResponse<T>(int whattodo, int code, string msg, object record = null!)
        {
            object result;

            switch (whattodo)
            {
                case 1:
                    result_response _result = new result_response()
                    {
                        result_code = code,
                        result_msg = msg,
                        details = record == null ? "" : record?.ToString()
                    };
                    result = _result;
                    break;
                //case 2:
                //    result_userresponse _result = new result_response()
                //    {
                //        result_code = code,
                //        result_msg = msg,
                //        details = record == null ? "" : record?.ToString()
                //    };
                //    break;
                default:
                    base_response _base_response = new base_response()
                    {
                        result_code = code,
                        result_msg = msg
                    };
                    result = _base_response;
                    break;
            }
            return (T)result;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (userManager != null)
                {
                    userManager.Dispose();
                    userManager = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
