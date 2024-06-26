using InventoryManagement.CQRS.Commands.Interfaces;

namespace InventoryManagement.CQRS.Commands
{
    public class UpdateProductCommand : ICommand
    {
        public Guid AggregateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public UpdateProductCommand(Guid aggregateId, string name, string description, decimal price, int stock)
        {
            AggregateId = aggregateId;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}