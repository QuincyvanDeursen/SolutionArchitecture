using OrderService.Services.Mappers;
using OrderService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.Models;
using Shared.Models.Customer;
using Shared.Models.Inventory;
using Shared.Models.Payment;
using JsonSerializer = System.Text.Json.JsonSerializer;
using OrderEntityMapperExtensions = OrderService.Services.Mappers.OrderEntityMapperExtensions;

namespace OrderService.Services.RabbitMQ;

public class OrderMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "customer.create",
            "customer.update",
            "payment.create",
            "payment.update",
            "product.create",
            "product.update"
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // Create a new scope for the event handler service (scoped service)
        using var scope = serviceProvider.CreateScope();
        var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();

        switch (data.Topic)
        {
            case "customer.create":
                var createCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await eventHandlerService.ProcessCustomerCreatedEvent(createCustomer.ToOrderCustomer());
                break;
            case "customer.update":
                var updateCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await eventHandlerService.ProcessCustomerUpdatedEvent(updateCustomer.ToOrderCustomer());
                break;
            case "payment.create":
                var createPayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessPaymentCreatedEvent(createPayment.ToOrderPayment());
                break;
            case "payment.update":
                var updatePayment = JsonSerializer.Deserialize<Payment>(data.DataJson);
                await eventHandlerService.ProcessPaymentUpdatedEvent(updatePayment.ToOrderPayment());
                break;
            case "product.create":
                var createProduct = JsonSerializer.Deserialize<Product>(data.DataJson);
                await eventHandlerService.ProcessProductCreatedEvent(createProduct.ToOrderProduct());
                break;
            case "product.update":
                var updateProduct = JsonSerializer.Deserialize<Product>(data.DataJson);
                await eventHandlerService.ProcessProductUpdatedEvent(updateProduct.ToOrderProduct());
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