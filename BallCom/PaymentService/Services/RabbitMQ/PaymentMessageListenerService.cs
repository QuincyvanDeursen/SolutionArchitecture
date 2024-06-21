using System.Text.Json;
using PaymentService.Services.Interfaces;
using PaymentService.Services.Mapper;
using PaymentService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.Models;

namespace PaymentService.Services.RabbitMQ;

public class PaymentMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "order.create",
            "customer.create",
            "customer.update",
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // Create a new scope for the event handler service (scoped service)
        using var scope = serviceProvider.CreateScope();
        var orderEventHandlerService = scope.ServiceProvider.GetRequiredService<IOrderEventHandlerService>();  
        var customerEventHandlerService = scope.ServiceProvider.GetRequiredService<ICustomerEventHandlerService>();
        
        switch (data.Topic)
        {
            case "customer.create":
                var createdCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await customerEventHandlerService.ProcessCustomerCreateEvent(PaymentRelatedEntityMapper.MapCustomerToPaymentCustomer(createdCustomer));
                break;
            case "customer.update":
                var updatedCustomer = JsonSerializer.Deserialize<Customer>(data.DataJson);
                await customerEventHandlerService.ProcessCustomerUpdateEvent(PaymentRelatedEntityMapper.MapCustomerToPaymentCustomer(updatedCustomer));
                break;
            case "order.create":
                var createdOrder = JsonSerializer.Deserialize<Order>(data.DataJson);
                await orderEventHandlerService.ProcessOrderCreateEvent(PaymentRelatedEntityMapper.MapOrderToPaymentOrder(createdOrder));
                break;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }
}