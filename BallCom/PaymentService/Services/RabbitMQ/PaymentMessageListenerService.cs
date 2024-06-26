using System.Text.Json;
using PaymentService.Services.Interfaces;
using PaymentService.Services.Mappers;
using PaymentService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.Models;
using Shared.Models.Customer;
using Shared.Models.Order;

namespace PaymentService.Services.RabbitMQ;

public class PaymentMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "order.created",
            "order.cancelled",
            "order.statusUpdated",
            "customer.created",
            "customer.updated",
        });
    }

    private async Task OnMessageReceived(MessageEventData data)
    {
        // Create a new scope for the event handler service (scoped service)
        using var scope = serviceProvider.CreateScope();
        var orderEventHandlerService = scope.ServiceProvider.GetRequiredService<IOrderEventHandlerService>();  
        var customerEventHandlerService = scope.ServiceProvider.GetRequiredService<ICustomerEventHandlerService>();
        
        switch (data.Topic)
        {
            case "customer.created":
                var createdCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await customerEventHandlerService.ProcessCustomerCreateEvent(PaymentRelatedEntityMapper.MapCustomerToPaymentCustomer(createdCustomer));
                break;
            case "customer.updated":
                var updatedCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await customerEventHandlerService.ProcessCustomerUpdateEvent(PaymentRelatedEntityMapper.MapCustomerToPaymentCustomer(updatedCustomer));
                break;
            case "order.created":
                var createdOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                await orderEventHandlerService.ProcessOrderCreateEvent(PaymentRelatedEntityMapper.MapOrderToPaymentOrder(createdOrder));
                break;
            case "order.cancelled":
                var cancelledOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                await orderEventHandlerService.ProcessOrderUpdateEvent(PaymentRelatedEntityMapper.MapOrderToPaymentOrder(cancelledOrder));
                break;
            case "order.statusUpdated":
                var updatedOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                await orderEventHandlerService.ProcessOrderUpdateEvent(PaymentRelatedEntityMapper.MapOrderToPaymentOrder(updatedOrder));
                break;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.DisconnectAsync();
    }
}