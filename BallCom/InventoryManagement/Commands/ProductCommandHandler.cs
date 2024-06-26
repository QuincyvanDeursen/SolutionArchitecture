

using InventoryManagement.Domain;
using InventoryManagement.Events;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

namespace InventoryManagement.Commands
{
    public class ProductCommandHandler
    {
        private readonly ILogger<ProductCommandHandler> _logger;
        private readonly IWriteRepository<Event> _eventStore; //write DB naar om events op te slaan
        private readonly IMessagePublisher _messagePublisher; //message broker om events te publiceren
        private readonly IReadRepository<Product> _productReadRepo; //read DB om producten op te halen

        public ProductCommandHandler(IWriteRepository<Event> eventStore, IMessagePublisher messagePublisher, IReadRepository<Product> productReadRepo, ILogger<ProductCommandHandler> logger)
        {
            _eventStore = eventStore;
            _messagePublisher = messagePublisher;
            _productReadRepo = productReadRepo;
            _logger = logger;
        }

        public async Task Handle(CreateProductCommand command)
        {
            // Voer validatie uit indien nodig...

            // Maak een nieuw product aan
            var product = new Product(Guid.NewGuid(), command.Name, command.Description, command.Price, command.Stock);

            // Sla het event op (aggregateId is het Id van het product)
            var @event = new ProductCreatedEvent(product.Id, product);
            await _eventStore.CreateAsync(@event);

            // Publiceer het event naar de message broker
            await _messagePublisher.PublishAsync(@event.Product, "product.created");
        }

        public async Task Handle(IncreaseStockCommand command)
        {
            _logger.LogInformation($"Aangekomen bij increasestock command");
            _logger.LogInformation($"Aangekomen bij increasestock command {command.AggregateId}");
            // Voer validatie uit indien nodig...

            // Haal het product op uit de database
            var product = await _productReadRepo.GetByIdAsync(command.AggregateId);

            if (product == null)
            {
                throw new Exception("Product which stock should be increased not found");
            }
            _logger.LogInformation($"Aangekomen voorbij db call command");

            // ze het aantal toegevoegde items als de stock van het product (wordt later opgeteld)
            product.Stock = command.Amount;

            // Sla het event op
            var @event = new StockIncreasedEvent(product.Id, product);
            await _eventStore.CreateAsync(@event);

            // Publiceer het event naar de message broker
            await _messagePublisher.PublishAsync(@event.Product, "product.stockincreased");
        }
    }
}
