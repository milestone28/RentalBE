

namespace Rental.Domain.Constants
{
   public class AuthConstant
    {
        public class Roles
        {
            public const string admin = "Admin";
            public const string owner = "Owner";
            public const string user = "Renter";
            public const string adminId = "8fe14426-55e9-4f04-b0f3-6e4791487f9c";
            public const string ownerId = "f8c36cca-99ad-4735-a180-dfab14d5415a";
            public const string renterId = "AC605A17-69D6-4A45-BC42-30D6DE7CE0B2";
        }

        public class defaultAdmin
        {
            public const string adminEmail = "admin@test.com";
            public const string adminPassword = "P@ssw0rd";
            public const string adminId = "f7dd87f9-6dce-459d-a981-c8ce5787d938";
        }

        public class defaultOwner
        {
            public const string ownerEmail = "owner@test.com";
            public const string ownerPassword = "ownerTest";
            public const string ownerId = "289AA786-650A-448B-8D90-5D7F9B97DB9A";
        }
    }
}
