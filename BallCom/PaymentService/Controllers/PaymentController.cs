using Microsoft.AspNetCore.Mvc;
using PaymentService.Dto;
using PaymentService.Services.Interfaces;
using Shared.Models;
using Shared.Repository.Interface;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(
        ILogger<PaymentController> logger,
        IPaymentService paymentService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await paymentService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var invoice = await paymentService.Get(id);
                if (invoice == null) return NotFound();

                return Ok(invoice);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentUpdateDto paymentUpdateDto)
        {
            try
            {
                await paymentService.Update(id, paymentUpdateDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
