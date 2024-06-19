using CustomerService.Domain;
using CustomerService.Dto;
using CustomerService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                var result = await _customerService.GetAll();
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            try
            {
                var result = await _customerService.Get(id);
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerCreateDto customer)
        {
            try
            {
                await _customerService.Add(customer);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] CustomerUpdateDto customer, Guid id)
        {
            try
            {
                await _customerService.Update(id, customer);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _customerService.Delete(id);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
