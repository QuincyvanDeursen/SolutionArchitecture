using InventoryService.Domain;
using InventoryService.Events;
using InventoryService.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Endpoints
{
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryRepo _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryRepo inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public Inventory Get()
        {
            return _inventoryService.GetInventory(1);
        }

        [HttpGet]
        public Inventory Get(int id)
        {
            return _inventoryService.GetInventory(id);
        }

        [HttpPost]
        public void Post([FromBody] InventoryEvent inventoryEvent)
        {
            _inventoryService.SaveEvent(inventoryEvent);
        }
    }
}