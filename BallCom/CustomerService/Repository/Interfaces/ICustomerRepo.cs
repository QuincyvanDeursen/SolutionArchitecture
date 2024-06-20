using CustomerService.Domain;

namespace CustomerService.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Guid id);
    }
}
