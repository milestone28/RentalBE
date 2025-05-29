using Microsoft.EntityFrameworkCore;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Infrastructure.Persistence;
using Tools;

namespace Rental.Infrastructure.Seeders
{
    internal class DefaultSeeders(AppDBContext _appDbContext) : IDefaultSeeders
    {
        public async Task GetSeed()
        {
            if (_appDbContext.Database.GetPendingMigrations().Any())
            {
                // Apply all pending migrations to the database
                await _appDbContext.Database.MigrateAsync();
                OnCreateUser();
            }
        }

        #region "private methods"
        private async void OnCreateUser()
        {
            DateTime datetimenow = DateTime.UtcNow.ToLocalTime();
            string remoteIpAddress = "0.0.0.1";
            var salt_builder = Convert.ToBase64String(Hasher.getSalt());
            var encryptpass = Hasher.GetPasswordHash(salt_builder, AuthConstant.defaultAdmin.password);
            var user = new User
            {
                id = Guid.NewGuid(),
                user_id = AuthConstant.defaultAdmin.user_id,
                salt = salt_builder,
                password = encryptpass,
                email = AuthConstant.defaultAdmin.email,
                mobile = "",
                first_name = "Gary",
                middle_name = "",
                last_name = "Yu",
                status = true,
                is_admin = true,
                is_owner = false,
                created_by = "system_defaul",
                created_date = datetimenow,
                updated_by = "system_defaul",
                updated_date = datetimenow,
                ip_lock = false,
                last_online = datetimenow,
                extra1 = "",
                extra2 = "",
                extra3 = "",
                extra4 = "",
                notes1 = "",
                notes2 = "",
                notes3 = "",
                notes4 = ""
            };
            await _appDbContext.users_.AddAsync(user);
            await _appDbContext.users_ip_.AddAsync(new User_IP
            {
                user_id = AuthConstant.defaultAdmin.user_id,
                user_ip = remoteIpAddress,
                created_by = "system_default",
                created_date = datetimenow,
                updated_by = "system_default",
                updated_date = datetimenow
            });
            await _appDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
