

namespace Rental.Domain.Constants
{
   public class UserConstant
    {
        public class Roles
        {
            public const string Admin = "Admin";
            public const string Owner = "Owner";
            public const string Renter = "Renter";
            public const string AdminId = "8fe14426-55e9-4f04-b0f3-6e4791487f9c";
            public const string OwnerId = "f8c36cca-99ad-4735-a180-dfab14d5415a";
            public const string RenterId = "f8c36cca-99ad-4735-a180-dfab14d5415a";
        }

        public class defaultAdmin
        {
            public const string adminEmail = "admin@test.com";
            public const string adminPassword = "P@ssw0rd";
            public const string AdminId = "f7dd87f9-6dce-459d-a981-c8ce5787d938";
        }
    }
}
