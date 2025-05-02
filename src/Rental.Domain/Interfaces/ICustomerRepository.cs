using Rental.Domain.Entities;

namespace Rental.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomersById(Guid id);
        Task<Guid> CreateCustomer(Customer customer);
        Task DeleteAsync(Customer restaurant);
        Task SaveChanges();
    }
}
