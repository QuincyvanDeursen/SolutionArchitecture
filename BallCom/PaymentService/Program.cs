using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using PaymentService.Repository;
using PaymentService.Services.Interfaces;
using PaymentService.Services.RabbitMQ;
using PaymentService.Services.RabbitMQ.EventHandlers;
using PaymentService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Models.Payment;
using Shared.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add database context service
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PaymentDbContext>(
    options => options.UseSqlServer(connectionString));

// Add Repository services
builder.Services.AddScoped<IWriteRepository<PaymentCustomer>, CustomerWriteRepo>();
builder.Services.AddScoped<IWriteRepository<PaymentOrder>, OrderWriteRepo>();
builder.Services.AddScoped<IWriteRepository<Payment>, PaymentWriteRepo>();
builder.Services.AddScoped<IReadRepository<Payment>, PaymentReadRepo>();

// Add Event handler services
builder.Services.AddScoped<IOrderEventHandlerService, OrderEventHandlerService>();
builder.Services.AddScoped<ICustomerEventHandlerService, CustomerEventHandlerService>();

// Add Microservice services
builder.Services.AddScoped<IPaymentService, PaymentService.Services.PaymentService>();

// Add RabbitMQ Publisher and Consumer services.
var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));

// Add hosted service for listening to RabbitMQ messages.
builder.Services.AddHostedService<PaymentMessageListenerService>();

builder.Services.AddControllers();

// Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Run migrations if in production
if (builder.Environment.IsProduction())
{
    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    dbContext.Database.Migrate();
}

var app = builder.Build();

// Enable Swagger middleware (endpoint: /swagger/index.html)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
