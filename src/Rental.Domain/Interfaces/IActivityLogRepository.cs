

using Rental.Domain.Entities;

namespace Rental.Domain.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<bool> LogActivity(Activitylogs logs);
    }
}
