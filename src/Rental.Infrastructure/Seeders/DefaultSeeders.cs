

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Infrastructure.Persistence;

namespace Rental.Infrastructure.Seeders
{
    internal class DefaultSeeders(RentalDBContext _dbContext, UserManager<User> _userManager) : IDefaultSeeders
    {
        public async Task SeedAsync()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }

            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

           await SeedUsersAsync();
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole { Name = UserConstant.Roles.Admin, NormalizedName = UserConstant.Roles.Admin.ToUpper() },
                new IdentityRole { Name = UserConstant.Roles.User, NormalizedName = UserConstant.Roles.User.ToUpper() }
            };
        }


        public async Task SeedUsersAsync()
        {
            var adminUser = await _userManager.FindByEmailAsync(UserConstant.defaultAdmin.adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = UserConstant.defaultAdmin.adminEmail,
                    Email = UserConstant.defaultAdmin.adminEmail
                };

                var result = await _userManager.CreateAsync(adminUser, UserConstant.defaultAdmin.adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
