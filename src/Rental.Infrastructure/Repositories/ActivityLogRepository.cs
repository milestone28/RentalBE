

using Microsoft.Extensions.Logging;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;
using Tools;

namespace Rental.Infrastructure.Repositories
{
    internal class ActivityLogRepository(AppDBContext _dbContext, ILogger<ActivityLogRepository> _logger) : IActivityLogRepository
    {
        public async Task<bool> LogActivity(Activitylogs logs)
        {
            bool result = false;
            try
            {
                await _dbContext.activitiy_logs_.AddAsync(logs);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("save_event: by " + logs.user_id + Helper.newLine + logs.activity_description + " " + ex.ToString());
                throw;
            }
            return result;
        }
    }
}
