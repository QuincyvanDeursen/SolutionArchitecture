﻿using InventoryManagement.CQRS.Commands;
using InventoryManagement.CQRS.Commands.Handler;
using InventoryManagement.Domain;
using InventoryManagement.Events;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using System.Text.Json;
using Shared.Models.Order;

namespace InventoryManagement.RabbitMQ
{
    public class ProductMessageListenerService : IHostedService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductMessageListenerService> _logger;

        public ProductMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider,
            ILogger<ProductMessageListenerService> logger)
        {
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageConsumer.ConsumeAsync(OnMessageReceived, new[]
            {
                "product.created",
                "product.updated",
                "product.stockincreased",
                "product.stockdecreased",
                "order.created",
                "order.cancelled"
            });
        }

        public async Task OnMessageReceived(MessageEventData data)
        {
            // Create a new scope for the event handler service (scoped service)
            using var scope = _serviceProvider.CreateScope();
            var eventHandler = scope.ServiceProvider.GetRequiredService<IEventHandler>();
            var commandHandler = scope.ServiceProvider.GetRequiredService<ProductCommandHandler>();

            switch (data.Topic)
            {
                case "product.created":
                {
                    _logger.LogInformation("Product created event received");
                    var product = JsonSerializer.Deserialize<Product>(data.DataJson);
                    _logger.LogInformation(data.DataJson);
                    await eventHandler.HandleProductCreatedAsync(product);
                }
                    break;
                case "product.updated":
                {
                    _logger.LogInformation("Product updated event received");
                    var product = JsonSerializer.Deserialize<Product>(data.DataJson);
                    await eventHandler.HandleProductUpdatedAsync(product);
                }
                    break;
                case "product.stockincreased":
                {
                    _logger.LogInformation("Product increased event received");
                    var product = JsonSerializer.Deserialize<Product>(data.DataJson);
                    await eventHandler.HandleStockIncreasedAsync(product.Id);
                }
                    break;
                case "product.stockdecreased":
                {
                    _logger.LogInformation("Product decreased event received");
                    var product = JsonSerializer.Deserialize<Product>(data.DataJson);
                    await eventHandler.HandleStockDecreasedAsync(product.Id);
                }
                    break;
                case "order.created":
                    // Do nothing, this is a dummy event
                    _logger.LogInformation("Order created event received");
                    var order = JsonSerializer.Deserialize<Order>(data.DataJson);

                    foreach (var item in order.OrderItems)
                    {
                        _logger.LogInformation($"Product Id: {item.ProductId}, Amount: {item.Quantity}");
                        await commandHandler.Handle(new DecreaseStockCommand(item.ProductId, item.Quantity));
                    }
                    break;
                case "order.cancelled":
                    // Do nothing, this is a dummy event
                    _logger.LogInformation("Dummy order cancelled event received");
                    var cancelledOrder = JsonSerializer.Deserialize<Order>(data.DataJson);

                    foreach (var item in cancelledOrder.OrderItems)
                    {
                        _logger.LogInformation($"Product Id: {item.ProductId}, Amount: {item.Quantity}");
                        await commandHandler.Handle(new IncreaseStockCommand(item.ProductId, item.Quantity));
                    }
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