using CustomerService.Domain;
using CustomerService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerEFRepo _customerService;
        public CustomerController(ILogger<CustomerController> logger, CustomerEFRepo customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerService.GetCustomers();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customerService.GetCustomer(id);
        }

        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _customerService.UpdateCustomer(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _customerService.DeleteCustomer(id);
        }
    }
}
