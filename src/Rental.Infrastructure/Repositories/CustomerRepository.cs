using Microsoft.EntityFrameworkCore;
using Rental.Domain.Constant;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Rental.Infrastructure.Repositories
{
    internal class CustomerRepository(RentalDBContext _dbContext) : ICustomerRepository
    {
        public async Task<(IEnumerable<Customer>, int)> GetAllCustomers(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, bool isDeleted)
        {
            string searchPhraseLower = string.IsNullOrEmpty(searchPhrase) ? "" : searchPhrase.ToLower();
            var baseQuery = _dbContext.Customers.Where(r=> searchPhrase == null && r.IsDeleted == isDeleted || (r.Name.ToLower().Contains(searchPhraseLower)) && r.IsDeleted == isDeleted);
            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {

                sortBy = sortBy!.ToLower();
                //sortBy = string.Concat(sortBy[0].ToString().ToUpper(), sortBy.AsSpan(1));

                var columnSelector = new Dictionary<string, Expression<Func<Customer, object>>>
                {
                    { nameof(Customer.Name).ToLower() , r => r.Name },
                    { "datecreated", r => r.DateCreated }
                };

                var selectedColumn = columnSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var _limitTotalPage = (int)Math.Ceiling((double)totalCount / pageSize);

            if (pageNumber > _limitTotalPage)
            {
                pageNumber = _limitTotalPage;
            }

            var customers = await baseQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
            return (customers, totalCount);
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
