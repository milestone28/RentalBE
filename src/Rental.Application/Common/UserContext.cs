

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Rental.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            var user_name = identity.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
            //var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
            return new CurrentUser(user_name);
        }
    }
}
