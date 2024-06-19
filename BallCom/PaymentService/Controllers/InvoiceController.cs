using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain;
using PaymentService.Dto;
using PaymentService.Repository.Interfaces;
using PaymentService.Services.Interfaces;

namespace PaymentService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {

        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceRepo invoiceRepo, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceRepo = invoiceRepo;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IEnumerable<Invoice> Get()
        {
            return _invoiceRepo.GetInvoices();
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> Get(Guid id)
        {
            var invoice = _invoiceRepo.GetInvoice(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InvoiceCreateDto invoiceCreateDto)
        {
            try
            {
                _logger.LogInformation("Adding new product");

                await _invoiceService.CreateInvoice(invoiceCreateDto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public void Put([FromBody] Invoice invoice)
        {
            _invoiceRepo.UpdateInvoice(invoice);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _invoiceRepo.DeleteInvoice(_invoiceRepo.GetInvoice(id));
        }
    }
}
