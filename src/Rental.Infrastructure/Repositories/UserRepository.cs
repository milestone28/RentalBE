

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;
using System.Text.Json;
using Tools;
using Tools.Models.Response;

namespace Rental.Infrastructure.Repositories
{
    internal class UserRepository(ILogger<UserRepository> _logger, UserManager<User> _userManager, AuthDBContext _authDBContext) : IUserRepository
    {
        public async Task<result_response> AddUser(User user_request, string password_request, string user_auth, string credentials_value)
        {
            result_response result = new result_response();
            User _new_user = new User();
            string userDetails = "";
            DateTime datetimenow = DateTime.Now;
            try
            {
                var checkToken = _authDBContext.SetupJWTs.AsNoTracking().FirstOrDefault(x => x.user_id == user_auth && x.user_token == credentials_value);
                if(checkToken == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Token, ReturnMessage.Authorization);
                    return result;
                }

                // validate UserID
                if (!Helper.isAlphaNumeric(user_request.UserName!))
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserIdSpecialCharacter);
                    return result;
                }

                // check Access Right is correct
                if ((user_request.is_user) && !user_request.is_owner && !user_request.is_user)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.AccessRights);
                    return result;
                }
               

                //override rights
                if(user_auth == AuthConstant.Roles.admin)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = true;
                    user_request.is_user = false;
                }
                else if (user_request.is_owner)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = false;
                    user_request.is_user = true;
                }
                else if (user_request.is_user)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = false;
                    user_request.is_user = false;
                }

                _new_user = new User()
                {
                    UserName = user_request.UserName,
                    Email = user_request.Email,
                    first_name = user_request.first_name,
                    last_name = user_request.last_name,
                    profile_image_url = user_request.profile_image_url,
                    is_admin = user_request.is_admin,
                    is_owner = user_request.is_owner,
                    is_user = user_request.is_user,
                    status = user_request.status,
                    PhoneNumber = user_request.PhoneNumber,
                    date_of_birth = user_request.date_of_birth,
                    last_online = datetimenow,
                    address = user_request.address,
                    macaddress_lock = false,
                    is_deleted = false,
                    base_model = new BaseModel()
                    {
                        created_by = user_auth,
                        created_date = datetimenow,
                        updated_by = user_auth,
                        updated_date = datetimenow,
                        extra1 = "",
                        extra2 = "",
                        extra3 = "",
                        extra4 = "",
                        notes1 = "",
                        notes2 = "",
                        notes3 = "",
                        notes4 = "",
                    }
                };

                var identityResult = await _userManager.CreateAsync(_new_user, password_request);
                if (!identityResult.Succeeded)
                {
                    // Check for duplicate user error
                    if (identityResult.Errors.Any(e => e.Code.Contains("duplicate", StringComparison.OrdinalIgnoreCase)))
                    {
                        result = CreateResponse<result_response>(1, (int)ReturnCode.Error, identityResult.Errors.FirstOrDefault()!.Description.ToString()!);
                        return result;
                    }
                    else
                    {
                        // Collect all error messages and return them
                        var errorMessages = identityResult.Errors.Select(e => e.Description).ToList();
                        string jsonResult = JsonSerializer.Serialize(errorMessages);
                        result = CreateResponse<result_response>(1, (int)ReturnCode.Error, jsonResult);
                        return result;
                    }
                }

                switch (user_auth)
                    {
                        case AuthConstant.Roles.admin:
                            identityResult = await _userManager.AddToRoleAsync(user_request, AuthConstant.Roles.owner);
                            break;
                        case AuthConstant.Roles.owner:
                            identityResult = await _userManager.AddToRoleAsync(user_request, AuthConstant.Roles.user);
                            break;
                        default:
                            identityResult = await _userManager.AddToRoleAsync(user_request, AuthConstant.Roles.user);
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
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null!;
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
