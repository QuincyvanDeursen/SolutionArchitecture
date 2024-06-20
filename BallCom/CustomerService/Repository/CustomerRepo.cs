using CustomerService.Database;
using CustomerService.Domain;
using CustomerService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CustomerDbContext _context;

        public CustomerRepo(CustomerDbContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

        }
    }
}
