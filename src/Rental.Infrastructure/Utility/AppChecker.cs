

using Rental.Domain.Entities;

namespace Rental.Infrastructure.Utility
{
    public static class AppChecker
    {
        public static bool has_right(User toChange, User authUser)
        {
            bool has_right = false;

            if (toChange.is_admin && authUser.is_admin)
            {
                return has_right;
            }
            else if (authUser.is_user)
            {
                if (toChange.is_user)
                {
                    return has_right;
                }
            }
            else if (toChange.is_admin && authUser.is_owner)
            {
                return has_right;
            }
            else if (authUser.is_user)
            {
                if (toChange.is_admin)
                {
                    return has_right;
                }
            }

            has_right = true;
            return has_right;
        }
    }
}
