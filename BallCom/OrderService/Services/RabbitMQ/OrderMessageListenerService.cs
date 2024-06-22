using OrderService.Domain;
using OrderService.Services.Interface;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OrderService.Services.RabbitMQ;

public class OrderMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "payment.create",
            "payment.update"
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // Create a new scope for the event handler service (scoped service)
        using var scope = _serviceProvider.CreateScope();
        var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();

        switch (data.Topic)
        {
            case "payment.create":
                var createPayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessPaymentCreatedEvent(createPayment);
                break;
            case "payment.update":
                var updatePayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessProductUpdatedEvent(updatePayment);
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