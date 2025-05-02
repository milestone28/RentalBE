using Microsoft.EntityFrameworkCore;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;

namespace Rental.Infrastructure.Repositories
{
    internal class CustomerRepository(RentalDBContext _dbContext) : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return customers;
        }
        public async Task<Customer?> GetCustomersById(Guid id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<Guid> CreateCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task DeleteAsync(Customer restaurant)
        {
            _dbContext.Customers.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
        }
        public Task SaveChanges()
        => _dbContext.SaveChangesAsync();
    }
}
