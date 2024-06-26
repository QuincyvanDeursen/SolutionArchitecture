using InventoryManagement.Events;

namespace InventoryManagement.Domain
{
    public class Product
    {
    public Guid Id { get;  set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public decimal Price { get;  set; }
    public int Stock { get;  set; }

        public Product()
        {
        }

        // Constructor om een nieuw product te maken
        public Product(Guid id, string name, string description, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        // Methode om een event toe te passen en de huidige staat bij te werken
        public void Apply(ProductCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Product.Name;
            Description = @event.Product.Description;
            Price = @event.Product.Price;
            Stock = @event.Product.Stock;
        }

        public void Apply(ProductUpdatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Product.Name;
            Description = @event.Product.Description;
            Price = @event.Product.Price;
            Stock = @event.Product.Stock;
        }

        public void Apply(StockIncreasedEvent @event)
        {
            Stock += @event.Product.Stock;
        }

        public void Apply(StockDecreasedEvent @event)
        {
            Stock -= @event.Product.Stock;
        }
    }
}
