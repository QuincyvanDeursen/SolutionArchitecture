using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain;
using PaymentService.Repository.Interfaces;

namespace PaymentService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {

        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceRepo _invoiceRepo;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceRepo invoiceRepo)
        {
            _logger = logger;
            _invoiceRepo = invoiceRepo;
        }

        [HttpGet]
        public IEnumerable<Invoice> Get()
        {
            return _invoiceRepo.GetInvoices();
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> Get(int id)
        {
            var invoice = _invoiceRepo.GetInvoice(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpPost]
        public void Post([FromBody] Invoice invoice)
        {
            _invoiceRepo.SaveInvoice(invoice);
        }

        [HttpPut]
        public void Put([FromBody] Invoice invoice)
        {
            _invoiceRepo.UpdateInvoice(invoice);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _invoiceRepo.DeleteInvoice(_invoiceRepo.GetInvoice(id));
        }
    }
}
