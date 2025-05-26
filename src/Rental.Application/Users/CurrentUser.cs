

namespace Rental.Application.Users
{
   public record CurrentUser(string Id,string Name, string Email, IEnumerable<string> Roles)
    {
        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}
