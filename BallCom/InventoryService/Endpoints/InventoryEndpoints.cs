using Carter;
using InventoryService.Domain;
using InventoryService.Repository.Interface;

namespace InventoryService.Endpoints
{
    public class InventoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var inventoryEndpoint = app.MapGroup("/inventory")
                .WithOpenApi();

            inventoryEndpoint.MapGet("", (IInventoryRepo inventoryRepo) => inventoryRepo.GetInventories())
                .WithSummary("Get all inventory");

            inventoryEndpoint.MapGet("{id:int}",
                    (IInventoryRepo inventoryRepo, int id) => inventoryRepo.GetInventory(id))
                .WithSummary("Get inventory by id");

            inventoryEndpoint.MapPost("",
                    (IInventoryRepo inventoryRepo, Inventory inventory) => inventoryRepo.AddInventory(inventory))
                .WithSummary("Add inventory");

            inventoryEndpoint.MapPut("",
                    (IInventoryRepo inventoryRepo, Inventory inventory) => inventoryRepo.UpdateInventory(inventory))
                .WithSummary("Update inventory");

            inventoryEndpoint.MapDelete("{id:int}",
                    (IInventoryRepo inventoryRepo, int id) => inventoryRepo.DeleteInventory(id))
                .WithSummary("Delete inventory");
        }
    }
}