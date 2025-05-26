

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rental.Domain.Response;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tools;
using Tools.Models.Response;
using static Rental.Domain.Constants.AuthConstant;

namespace Rental.Infrastructure.Repositories
{
    internal class AuthRepository(AuthDBContext _dBContext ,UserManager<User> userManager, IConfiguration configuration) : IAuthRepository
    {
        public async Task<login_user_response> Login(string username, string password, string userAgent)
        {
            login_user_response result = new login_user_response();

            try
            {
                var identityUser = await userManager.FindByNameAsync(username);
                if (identityUser == null)
                {
                    result = CreateResponse<login_user_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }

                var checkPassword = await userManager.CheckPasswordAsync(identityUser, password);
                if(!checkPassword)
                {
                    result = CreateResponse<login_user_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidAuthHeader);
                    return result;
                }

                // check if Active
                if(!identityUser.status)
                {
                    result = CreateResponse<login_user_response>(1, (int)ReturnCode.Error, ReturnMessage.UserDeactivated);
                    return result;
                }

                // add claim role here
                Claim[] claimsdata = new Claim[] { };

                if (identityUser.is_admin)
                {
                    claimsdata = new[] {
                        new Claim(ClaimTypes.Name, identityUser.UserName!),
                        new Claim(ClaimTypes.Role, Roles.admin),
                        new Claim("Device", userAgent)
                    };
                }
                else if (identityUser.is_owner)
                {
                    claimsdata = new[] {
                        new Claim(ClaimTypes.Name, identityUser.UserName!),
                        new Claim(ClaimTypes.Role, Roles.owner),
                        new Claim("Device", userAgent)
                    };
                }
                else
                {
                    claimsdata = new[] {
                        new Claim(ClaimTypes.Name, identityUser.UserName!),
                        new Claim(ClaimTypes.Role, Roles.renter),
                        new Claim("Device", userAgent)
                    };
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtValues").GetSection("secretkey").Value!));
                var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                   issuer: configuration.GetSection("JwtValues").GetSection("issuer").Value,
                   audience: configuration.GetSection("JwtValues").GetSection("audience").Value,
                   expires: DateTime.Now.AddDays(7),
                   claims: claimsdata,
                   signingCredentials: signInCred
                   );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                var mjwt = await _dBContext.SetupJWTs.FirstOrDefaultAsync(x => x.user_id == identityUser.UserName && x.device_id == userAgent);
                if(mjwt == null)
                {
                    mjwt = new SetupJWT()
                    {
                        user_id = identityUser.UserName!,
                        device_id = userAgent,
                        user_token = tokenString,
                        expire_in = token.ValidTo
                    };
                    await _dBContext.SetupJWTs.AddAsync(mjwt);
                }
                else
                {
                    mjwt.user_token = tokenString;
                    mjwt.expire_in = token.ValidTo;
                    _dBContext.SetupJWTs.Update(mjwt);
                }
                await _dBContext.SaveChangesAsync();
                result = new login_user_response()
                {
                    result_code = (int)ReturnCode.Success,
                    result_msg = ReturnMessage.Login,
                    details = new login_user_details()
                    {
                        user_id = identityUser.UserName!,
                        token = tokenString,
                        expire_in = token.ValidTo,
                        device_id = userAgent,
                        is_admin = identityUser.is_admin,
                        is_owner = identityUser.is_owner,
                        is_user = identityUser.is_owner
                    }
                };

            }
            catch (Exception ex)
            {
                result = CreateResponse<login_user_response>(1, (int)ReturnCode.Error, ex.Message);
                return result;
            }

            return result;
        }

        private T CreateResponse<T>(int whattodo, int code, string mssg, object record = null!)
        {
            object result;
            switch (whattodo)
            {
                case 1:
                    login_user_response _login_user_response = new login_user_response()
                    {
                        result_code = code,
                        result_msg = mssg,
                        details = code == 0 ? (login_user_details)record : new login_user_details()
                    };
                    result = _login_user_response;
                    break;
                case 2:
                    result_response _writer_result = new result_response()
                    {
                        result_code = code,
                        result_msg = mssg,
                        details = ""
                    };
                    result = _writer_result;
                    break;
                default:
                    base_response _base_response = new base_response();
                    _base_response.result_code = code;
                    _base_response.result_msg = mssg;
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
                    userManager = null!;
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
