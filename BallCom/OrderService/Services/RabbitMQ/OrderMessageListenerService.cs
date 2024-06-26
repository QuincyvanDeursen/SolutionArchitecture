using OrderService.Extensions;
using OrderService.Services.Interface;
using OrderService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OrderService.Services.RabbitMQ;

public class OrderMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{

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
        using var scope = serviceProvider.CreateScope();
        var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();

        switch (data.Topic)
        {
            case "payment.create":
                var createPayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessPaymentCreatedEvent(createPayment.ToOrderPayment());
                break;
            case "payment.update":
                var updatePayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessPaymentUpdatedEvent(updatePayment.ToOrderPayment());
                break;
            default:
                throw new ArgumentException(data.Topic + " is not a subscribed topic.");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.DisconnectAsync();
    }
}