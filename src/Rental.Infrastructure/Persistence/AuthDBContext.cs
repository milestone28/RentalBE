

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Constants;
using Rental.Domain.Entities;

namespace Rental.Infrastructure.Persistence
{
    internal class AuthDBContext(DbContextOptions<AuthDBContext> options) : IdentityDbContext<User>(options)
    {

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Create Admin , Owner , Renter roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = UserConstant.Roles.AdminId,
                    Name = UserConstant.Roles.Admin,
                    NormalizedName = UserConstant.Roles.Admin.ToUpper(),
                    ConcurrencyStamp = UserConstant.Roles.AdminId
                },
                new IdentityRole
                {
                    Id = UserConstant.Roles.OwnerId,
                    Name = UserConstant.Roles.Owner,
                    NormalizedName = UserConstant.Roles.Owner.ToUpper(),
                    ConcurrencyStamp = UserConstant.Roles.OwnerId
                },
                 new IdentityRole
                {
                    Id = UserConstant.Roles.RenterId,
                    Name = UserConstant.Roles.Renter,
                    NormalizedName = UserConstant.Roles.Renter.ToUpper(),
                    ConcurrencyStamp = UserConstant.Roles.Renter
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUser = new User
            {
                Id = UserConstant.defaultAdmin.AdminId,
                Email = UserConstant.defaultAdmin.adminEmail,
                NormalizedEmail = UserConstant.defaultAdmin.adminEmail.ToUpper(),
                UserName = UserConstant.defaultAdmin.adminEmail,
                NormalizedUserName = UserConstant.defaultAdmin.adminEmail.ToUpper()
            };
            adminUser.PasswordHash = new PasswordHasher<User>().HashPassword(adminUser, UserConstant.defaultAdmin.adminPassword);
            modelBuilder.Entity<User>().HasData(adminUser);

            //Create Admin role for the user
            var adminRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = UserConstant.Roles.AdminId,
                    UserId = UserConstant.defaultAdmin.AdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId = UserConstant.Roles.RenterId,
                    UserId = UserConstant.defaultAdmin.AdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId = UserConstant.Roles.OwnerId,
                    UserId = UserConstant.defaultAdmin.AdminId
                }
            };

        }
    }
}
