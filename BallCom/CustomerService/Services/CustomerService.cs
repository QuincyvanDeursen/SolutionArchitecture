using CustomerService.Domain;
using CustomerService.Dto;
using CustomerService.Repository.Interfaces;
using CustomerService.Services.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;

namespace CustomerService.Services
{
    public class CustomerService(ICustomerRepo customerRepo, IMessagePublisher messagePublisher)
        : ICustomerService
    {
        public async Task<Customer> Get(Guid id)
        {
            var customer = await customerRepo.GetCustomer(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await customerRepo.GetAllCustomers();
            if (customers == null || !customers.Any())
            {
                throw new KeyNotFoundException("No customers found.");
            }
            return customers;
        }

        public async Task Delete(Guid id)
        {
            await customerRepo.DeleteCustomer(id);
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

            await customerRepo.AddCustomer(customer);
            
            // Send event to RabbitMQ
            await messagePublisher.PublishAsync(customer, "customer.created");
        }

        public async Task Update(Guid id, CustomerUpdateDto customer)
        {
            var existingCustomer = await customerRepo.GetCustomer(id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            existingCustomer.FirstName = customer.FirstName ?? existingCustomer.FirstName;
            existingCustomer.LastName = customer.LastName ?? existingCustomer.LastName;
            existingCustomer.PhoneNumber = customer.PhoneNumber ?? existingCustomer.PhoneNumber;
            existingCustomer.CompanyName = customer.CompanyName ?? existingCustomer.CompanyName;

            await customerRepo.UpdateCustomer(existingCustomer);
            
            // Send event to RabbitMQ
            await messagePublisher.PublishAsync(existingCustomer, "customer.updated");
        }
    }
}
