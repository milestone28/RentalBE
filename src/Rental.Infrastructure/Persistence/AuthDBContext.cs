

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Constants;
using Rental.Domain.Entities;

namespace Rental.Infrastructure.Persistence
{
    internal class AuthDBContext(DbContextOptions<AuthDBContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<SetupJWT> SetupJWTs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Creating Seeders for the database

            //Create Admin , Owner , Renter roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = AuthConstant.Roles.adminId,
                    Name = AuthConstant.Roles.admin,
                    NormalizedName = AuthConstant.Roles.admin.ToUpper(),
                    ConcurrencyStamp = AuthConstant.Roles.adminId
                },
                new IdentityRole
                {
                    Id = AuthConstant.Roles.ownerId,
                    Name = AuthConstant.Roles.owner,
                    NormalizedName = AuthConstant.Roles.owner.ToUpper(),
                    ConcurrencyStamp = AuthConstant.Roles.ownerId
                },
                 new IdentityRole
                {
                    Id = AuthConstant.Roles.renterId,
                    Name = AuthConstant.Roles.user,
                    NormalizedName = AuthConstant.Roles.user.ToUpper(),
                    ConcurrencyStamp = AuthConstant.Roles.renterId
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUser = CreateUser(AuthConstant.defaultAdmin.adminId, AuthConstant.defaultAdmin.adminEmail, AuthConstant.defaultAdmin.adminPassword, true);
            var ownernUser = CreateUser(AuthConstant.defaultOwner.ownerId, AuthConstant.defaultOwner.ownerEmail, AuthConstant.defaultOwner.ownerPassword, false);
            modelBuilder.Entity<User>().HasData(adminUser, ownernUser);
    
            //Create default Admin role for admin
            var adminRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = AuthConstant.Roles.adminId,
                    UserId = AuthConstant.defaultAdmin.adminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId = AuthConstant.Roles.ownerId,
                    UserId = AuthConstant.defaultOwner.ownerId
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRole);
            #endregion
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().OwnsOne(r => r.address);
            modelBuilder.Entity<User>().OwnsOne(r => r.base_model);
            modelBuilder.Entity<SetupJWT>().HasKey(s => s.id);
        }



        #region DbSet Properties
        private User CreateUser(string id, string email, string password, bool is_admin)
        {
            var user = new User
            {
                Id = id,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email == AuthConstant.defaultAdmin.adminEmail ? "Admin" : "Owner",
                NormalizedUserName = email == AuthConstant.defaultAdmin.adminEmail ? "ADMIN" : "OWNER",
                first_name = email == AuthConstant.defaultAdmin.adminEmail ? "Admin" : "Owner",
                last_name = email == AuthConstant.defaultAdmin.adminEmail ? "Admin" : "Owner",
                profile_image_url = "",
                status = true,
                is_admin = is_admin == false ?  false : true,
                is_owner = is_admin == false ?  true : false,
            };
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
            return user;
        }

        #endregion
    }
}
