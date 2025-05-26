namespace Rental.Infrastructure.Seeders
{
    public interface IDefaultSeeders
    {
        Task GetAllPendingMigration();
    }
}