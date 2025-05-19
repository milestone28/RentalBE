
namespace Tools
{
    public static class ReturnMessage
    {
        // 000
        public static readonly string Login = "Successfully login.";
        public static readonly string Refresh = "Successfully refresh.";
        public static readonly string Logout = "Successfully logout.";
        public static readonly string Fetched = "Successfully fetched.";
        public static readonly string Saved = "Successfully saved.";
        public static readonly string Updated = "Successfully updated.";
        public static readonly string Removed = "Successfully removed.";
        public static readonly string ValidTokenPassword = "Password token is valid.";
        public static readonly string PendingRegistration = "Your registration has successfully submitted, please wait for a confirmation on your email.";
        public static readonly string Requested = "Successfully requested.";

        // 001
        public static readonly string Authorization = "Missing/Invalid token.";
        public static readonly string MissingAuthHeader = "Missing authorization header.";
        public static readonly string InvalidAuthHeader = "Invalid authorization header.";

        // 001 Denied
        public static string AccessRights = "Insufficient access rights.";
        public static string DeniedIPLock = "IP Lock Activated, Access denied at the current IP.";
        public static string DeniedDayLock = "Day Lock Activated, Access denied at the moment.";
        public static string DeniedTimeLock = "Time Lock Activated, Access denied at the moment.";
        public static string DeniedMacAddress = "MAC Address is Activated, Access denied at the current mac address.";

        // 001 Others
        public static readonly string UserDeactivated = "User deactivated please contact administrator.";
        public static readonly string ForgetPasswordError = "If your email exists in our database, you will receive a password recovery link at your email address in a few minutes..";
        public static readonly string ExpiredTokenPassword = "Your password token has expired.";
        public static readonly string NotMatchConfirmPass = "Password and confirm password do not match.";
        public static readonly string WeakPassword = "Can't use password, too weak.";

        // 001 Invalid
        public static string InvalidRequestData = "Invalid request data.";
        public static string InvalidUserId = "Invalid userID.";

        // 500
        public static string ErrorException = "Something went wrong please contact administrator.";
    }
}
