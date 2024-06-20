using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Repository;
using OrderService.Repository.Interface;
using OrderService.Services.RabbitMQ;
using RabbitMQ.Client;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;
using OrderService.Services;
using OrderService.Services.Interface;
using Shared.MessageBroker.Connection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<OrderDbContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();

// Add RabbitMQ Publisher and Consumer services.
var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));

// Add a hosted service for listening to RabbitMQ messages (consumer).
builder.Services.AddHostedService<OrderMessageListenerService>();

builder.Services.AddScoped<IOrderService, OrderService.Services.RabbitMQ.OrderService>();
builder.Services.AddScoped<IInventoryServiceClient, InventoryServiceClient>();
builder.Services.AddControllers();

builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Run migrations if in production
if (builder.Environment.IsProduction())
{
    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

var app = builder.Build();

// Add swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
