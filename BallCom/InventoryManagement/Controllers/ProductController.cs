using InventoryManagement.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductCommandHandler _commandHandler;

        public ProductController(ProductCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {

            await _commandHandler.Handle(command);

            return Ok();
        }

        [HttpPost("increasestock")]
        public async Task<IActionResult> IncreaseStock(IncreaseStockCommand command)
        {

            await _commandHandler.Handle(command);

            return Ok();
        }
    }
}
