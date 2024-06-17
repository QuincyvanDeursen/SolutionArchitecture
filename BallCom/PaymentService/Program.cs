using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using PaymentService.Repository;
using PaymentService.Repository.Interfaces;
using PaymentService.Services.RabbitMQ;
using Shared.MessageBroker;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PaymentDbContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();

// Add RabbitMQ Publisher and Consumer services.
var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));

// Add hosted service for listening to RabbitMQ messages.
builder.Services.AddHostedService<PaymentMessageListenerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Add swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
