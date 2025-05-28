using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Persistence;

namespace Rental.Infrastructure.Seeders
{
    internal class DefaultSeeders(AppDBContext _appDbContext, IHttpContextAccessor _httpContextAccessor) : IDefaultSeeders
    {
        public async Task GetAllPendingMigration()
        {
            IEnumerable<string>[] pendingMigrations = [_appDbContext.Database.GetPendingMigrations(),_authDbContext.Database.GetPendingMigrations()];
            if (pendingMigrations.Any())
            {
                // Apply all pending migrations to the database
                await _authDbContext.Database.MigrateAsync();
                await _appDbContext.Database.MigrateAsync();
            }
        }
    }
}
