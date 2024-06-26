using InventoryManagement.Domain;
using InventoryManagement.Events;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using System.Text.Json;

namespace InventoryManagement.RabbitMQ
{
    public class ProductMessageListenerService : IHostedService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductMessageListenerService> _logger;

        public ProductMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider, ILogger<ProductMessageListenerService> logger)
        {
            
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageConsumer.ConsumeAsync(OnMessageReceived, new[]
            {
            "product.created","product.stockincreased"
        });
        }

        public async Task OnMessageReceived(MessageEventData data)
        {
            // Create a new scope for the event handler service (scoped service)
            using var scope = _serviceProvider.CreateScope();
            var eventHandler = scope.ServiceProvider.GetRequiredService<IEventHandler>();

            switch (data.Topic)
            {
                case "product.created":
                    _logger.LogInformation("Product created event received");
                    var product = JsonSerializer.Deserialize<Product>(data.DataJson);
                    _logger.LogInformation(data.DataJson);
                    await eventHandler.HandleProductCreatedAsync(product);
                    break;
                case "product.stockincreased":
                    _logger.LogInformation("Product increased event received");
                    var product2 = JsonSerializer.Deserialize<Product>(data.DataJson);
                    await eventHandler.HandlestockIncreasedAsync(product2.Id);
                    break;
                default:
                    throw new ArgumentException(data.Topic + " is not a subscribed topic.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // TODO: Implement the stop logic for the connection
            return Task.CompletedTask;
        }
    }
}
