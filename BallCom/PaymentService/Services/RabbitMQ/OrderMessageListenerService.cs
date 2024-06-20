using PaymentService.Domain;
using PaymentService.Services.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using System.Text.Json;

namespace PaymentService.Services.RabbitMQ
{
    public class OrderMessageListenerService(IMessageConsumer messageConsumer, IOrderEventHandlerService orderEventHandlerService, IServiceProvider serviceProvider)
    {
        private readonly IOrderEventHandlerService _orderEventHandlerService = orderEventHandlerService;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IMessageConsumer _messageConsumer = messageConsumer;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await messageConsumer.ConsumeAsync(OnMessageReceived, new[]
            {
                "order.create",
                "order.update",
                "customer.create",
                "customer.update"
        });
        }

        public async Task OnMessageReceived(MessageEventData data)
        {
            // Create a new scope for the event handler service (scoped service)
            using var scope = _serviceProvider.CreateScope();
            var eventHandlerService = scope.ServiceProvider.GetService<IOrderEventHandlerService>() ??
                                      throw new ArgumentNullException(nameof(IOrderEventHandlerService));

            switch (data.Topic)
            {
                case "inventory.create":
                    var createOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                    await _orderEventHandlerService.AddOrder(createOrder);
                    break;
                case "inventory.update":
                    var updateOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                    await _orderEventHandlerService.UpdateOrder(updateOrder);
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
