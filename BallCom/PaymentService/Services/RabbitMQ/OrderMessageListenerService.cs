using PaymentService.Domain;
using PaymentService.Services.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace PaymentService.Services.RabbitMQ
{
    public class OrderMessageListenerService(IMessageConsumer messageConsumer, IOrderEventHandlerService orderEventHandlerService)
    {
        private readonly IOrderEventHandlerService _orderEventHandlerService = orderEventHandlerService;
        private readonly IMessageConsumer _messageConsumer = messageConsumer;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await messageConsumer.ConsumeAsync<MessageEventData<object>>(OnMessageReceived, new[]
            {
                "order.create",
                "order.update",
                "customer.create",
                "customer.update"
            });
        }

        public async Task OnMessageReceived(MessageEventData<object> data)
        {
            switch (data.Topic)
            {
                case "inventory.create":
                    var createOrder = (Order)data.Data;
                    await _orderEventHandlerService.AddOrder(createOrder);
                    break;
                case "inventory.update":
                    var updateOrder = (Order)data.Data;
                    await _orderEventHandlerService.UpdateOrder(updateOrder);
                    break;
                default:
                    Console.WriteLine();
                    throw new ArgumentException(data.Topic + " is not a listed topic.");
            }
        }
    }
}
