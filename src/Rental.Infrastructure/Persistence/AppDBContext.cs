using Microsoft.EntityFrameworkCore;
using Rental.Domain.Entities;

namespace Rental.Infrastructure.Persistence
{
    internal class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        internal DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Customer>().OwnsOne(r => r.Address);
        }
    }
}
