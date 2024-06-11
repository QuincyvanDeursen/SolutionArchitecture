using InventoryService.Domain;
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
        public IEnumerable<Inventory> Get()
        {
            return _inventoryService.GetInventories();
        }

        [HttpGet]
        public Inventory Get(int id)
        {
            return _inventoryService.GetInventory(id);
        }

        [HttpPost]
        public void Post([FromBody] Inventory inventory)
        {
            _inventoryService.AddInventory(inventory);
        }

        [HttpGet]
        public void Put([FromBody] Inventory inventory)
        {
            _inventoryService.UpdateInventory(inventory);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _inventoryService.DeleteInventory(id);
        }
    }
}