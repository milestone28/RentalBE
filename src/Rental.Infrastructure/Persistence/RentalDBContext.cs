

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Entities;

namespace Rental.Infrastructure.Persistence
{
    public class RentalDBContext(DbContextOptions<RentalDBContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Customer>().OwnsOne(r => r.Address);
        }
    }
}
