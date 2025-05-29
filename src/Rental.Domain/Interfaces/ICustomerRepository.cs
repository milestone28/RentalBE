
using Rental.Domain.Entities;
using static Tools.Models.sort_direction;

namespace Rental.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<(IEnumerable<Customer>, int)> GetAllCustomers(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, bool isDeleted);
        Task<Customer?> GetCustomersById(Guid id);
        Task<Guid> CreateCustomer(Customer customer);
        Task DeleteAsync(Customer restaurant);
        Task SaveChanges();
    }
}
