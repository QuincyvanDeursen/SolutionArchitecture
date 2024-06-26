using InventoryManagement.Domain;
using Shared.Repository.Interface;
using System.Text.Json;

namespace InventoryManagement.Events
{
    public class EventHandler : IEventHandler
    {
        private readonly ILogger<EventHandler> _logger;
        private readonly IWriteRepository<Product> _productRepository;
        private readonly IReadRepository<Event> _productEventreadRepository;

        public EventHandler(IWriteRepository<Product> productRepository, IReadRepository<Event> productEventreadRepository, ILogger<EventHandler> logger)
        {
            _productRepository = productRepository;
            _productEventreadRepository = productEventreadRepository;
            _logger = logger;
        }

        public async Task HandleProductCreatedAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task HandleProductUpdatedAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task HandleStockIncreasedAsync(Guid aggregateId)
        {
            var product = await ReplayEvents(aggregateId);    
            await _productRepository.UpdateAsync(product);
        }

        public async Task HandleStockDecreasedAsync(Guid aggregateId)
        {
            var product = await ReplayEvents(aggregateId);
            await _productRepository.UpdateAsync(product);
        }

        private async Task<Product> ReplayEvents(Guid aggregateId)
        {
            var events = await _productEventreadRepository.GetAllByIdAsync(aggregateId);
            var productState = new Product();
            foreach (var @event in events)
            {

                var product = JsonSerializer.Deserialize<Product>(@event.Data);

                @event.Product = product;
                _logger.LogInformation("Event Data: {0}", @event.Data);
                switch (@event)
                {
                    case ProductCreatedEvent e:
                        productState.Apply(e);
                        break;
                    case ProductUpdatedEvent e:
                        productState.Apply(e);
                        break;
                    case StockIncreasedEvent e:
                        productState.Apply(e);
                        break;
                    case StockDecreasedEvent e:
                        productState.Apply(e);
                        break;
                    default:
                        throw new Exception("Unknown event type");
                }
            }


            return productState;
        }
    }
}
