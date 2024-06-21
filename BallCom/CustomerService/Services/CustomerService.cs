using CustomerService.Domain;
using CustomerService.Dto;
using CustomerService.Repository.Interfaces;
using CustomerService.Services.Interfaces;

namespace CustomerService.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }



        public async Task<Customer> Get(Guid id)
        {
            var customer = await _customerRepo.GetCustomer(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _customerRepo.GetAllCustomers();
            if (customers == null || !customers.Any())
            {
                throw new KeyNotFoundException("No customers found.");
            }
            return customers;
        }

        public async Task Delete(Guid id)
        {
            await _customerRepo.DeleteCustomer(id);
        }

        public async Task Create(CustomerCreateDto customerCreateDto)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = customerCreateDto.FirstName,
                LastName = customerCreateDto.LastName,
                PhoneNumber = customerCreateDto.PhoneNumber,
                CompanyName = customerCreateDto.CompanyName,
                Address = customerCreateDto.Address
            };

            await _customerRepo.AddCustomer(customer);

        }

        public async Task Update(Guid id, CustomerUpdateDto customer)
        {
            var existingCustomer = await _customerRepo.GetCustomer(id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            existingCustomer.FirstName = customer.FirstName ?? existingCustomer.FirstName;
            existingCustomer.LastName = customer.LastName ?? existingCustomer.LastName;
            existingCustomer.PhoneNumber = customer.PhoneNumber ?? existingCustomer.PhoneNumber;
            existingCustomer.CompanyName = customer.CompanyName ?? existingCustomer.CompanyName;

            await _customerRepo.UpdateCustomer(existingCustomer);
        }
    }
}
