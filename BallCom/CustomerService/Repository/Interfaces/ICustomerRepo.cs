using CustomerService.Domain;

namespace CustomerService.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
