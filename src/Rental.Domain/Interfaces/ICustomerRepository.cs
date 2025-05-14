using Rental.Domain.Constant;
using Rental.Domain.Entities;

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
