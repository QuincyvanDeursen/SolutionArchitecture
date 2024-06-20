using CustomerService.Domain;
using CustomerService.Dto;

namespace CustomerService.Services.Interfaces
{
    public interface ICustomerService
    {

        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Get(Guid id);
        Task Add(CustomerCreateDto customer);
        Task Update(Guid id, CustomerUpdateDto customer);
        Task Delete(Guid id);
    }
}
