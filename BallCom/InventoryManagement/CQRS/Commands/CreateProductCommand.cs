using InventoryManagement.CQRS.Commands.Interfaces;

namespace InventoryManagement.CQRS.Commands
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CreateProductCommand(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}