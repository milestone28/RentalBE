using Microsoft.EntityFrameworkCore;
using Rental.Domain.Entities;

namespace Rental.Infrastructure.Persistence
{
    internal class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        internal DbSet<Customer> customers_ { get; set; }
        internal DbSet<Activitylogs> activitiy_logs_ { get; set; }
        internal DbSet<SetupJWT> users_jwt_ { get; set; }
        internal DbSet<User> users_ { get; set; }
        internal DbSet<User_IP> users_ip_ { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            modelBuilder.Entity<Customer>().OwnsOne(r => r.Address);
            modelBuilder.Entity<User>().HasKey(x => x.id);
            modelBuilder.Entity<User>().OwnsOne(r => r.address);
            modelBuilder.Entity<SetupJWT>().HasKey(s => s.id);
            modelBuilder.Entity<User_IP>().HasKey(x => x.id);



            #region Creating Seeders for the database for microsoft identity

            //Create Admin , Owner , Renter roles
            //var roles = new List<IdentityRole>
            //{
            //    new IdentityRole
            //    {
            //        Id = AuthConstant.Roles.adminId,
            //        Name = AuthConstant.Roles.admin,
            //        NormalizedName = AuthConstant.Roles.admin.ToUpper(),
            //        ConcurrencyStamp = AuthConstant.Roles.adminId
            //    },
            //    new IdentityRole
            //    {
            //        Id = AuthConstant.Roles.ownerId,
            //        Name = AuthConstant.Roles.owner,
            //        NormalizedName = AuthConstant.Roles.owner.ToUpper(),
            //        ConcurrencyStamp = AuthConstant.Roles.ownerId
            //    },
            //     new IdentityRole
            //    {
            //        Id = AuthConstant.Roles.renterId,
            //        Name = AuthConstant.Roles.user,
            //        NormalizedName = AuthConstant.Roles.user.ToUpper(),
            //        ConcurrencyStamp = AuthConstant.Roles.renterId
            //    }
            //};

            //modelBuilder.Entity<IdentityRole>().HasData(roles);

            ////Create an Admin User
            //var adminUser = CreateUser(AuthConstant.defaultAdmin.adminId, AuthConstant.defaultAdmin.adminEmail, AuthConstant.defaultAdmin.adminPassword, true);
            //var ownernUser = CreateUser(AuthConstant.defaultOwner.ownerId, AuthConstant.defaultOwner.ownerEmail, AuthConstant.defaultOwner.ownerPassword, false);
            //modelBuilder.Entity<User>().HasData(adminUser, ownernUser);

            ////Create default Admin role for admin
            //var adminRole = new List<IdentityUserRole<string>>
            //{
            //    new IdentityUserRole<string>
            //    {
            //        RoleId = AuthConstant.Roles.adminId,
            //        UserId = AuthConstant.defaultAdmin.adminId
            //    },
            //      new IdentityUserRole<string>
            //    {
            //        RoleId = AuthConstant.Roles.ownerId,
            //        UserId = AuthConstant.defaultOwner.ownerId
            //    }
            //};
            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRole);
            #endregion



        }
    }
}
