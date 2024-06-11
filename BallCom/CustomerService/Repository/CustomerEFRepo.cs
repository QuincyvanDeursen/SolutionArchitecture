using CustomerService.Database;
using CustomerService.Domain;
using CustomerService.Repository.Interfaces;

namespace CustomerService.Repository
{
    public class CustomerEFRepo : ICustomerRepo
    {
        private readonly CustomerDbContext _context;

        public CustomerEFRepo(CustomerDbContext context) => _context = context;

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
