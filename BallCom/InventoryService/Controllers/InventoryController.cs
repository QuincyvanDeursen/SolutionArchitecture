using InventoryService.Domain;
using InventoryService.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Repository.Interface;

namespace InventoryService.Endpoints
{
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IReadRepository<Inventory> _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IReadRepository<Inventory> inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Inventory>> Get()
        {
            return await _inventoryService.GetAllAsync();
        }

        [HttpGet]
        public async Task<Inventory> Get(Guid id)
        {
            return await _inventoryService.GetByIdAsync(id);
        }

        [HttpPost]
        public void Post([FromBody] InventoryBaseEvent inventoryEvent)
        {
            // _inventoryService.SaveEvent(inventoryEvent);
        }
    }
}