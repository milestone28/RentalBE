using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Persistence;

namespace Rental.Infrastructure.Seeders
{
    internal class DefaultSeeders(AppDBContext _dbContext) : IDefaultSeeders
    {
        public async Task SeedAsync()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
    }
}
