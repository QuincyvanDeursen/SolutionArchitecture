using CustomerService.Domain;
using CustomerService.Repository;
using CustomerService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRepo _customerRepo;
        public CustomerController(ILogger<CustomerController> logger, ICustomerRepo customerRepo)
        {
            _logger = logger;
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerRepo.GetCustomers();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customerRepo.GetCustomer(id);
        }

        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _customerRepo.AddCustomer(customer);
        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _customerRepo.UpdateCustomer(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _customerRepo.DeleteCustomer(id);
        }
    }
}
