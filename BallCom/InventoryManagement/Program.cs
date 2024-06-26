using InventoryManagement.Commands;
using InventoryManagement.Database;
using InventoryManagement.Domain;
using InventoryManagement.Events;
using InventoryManagement.RabbitMQ;
using InventoryManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ProductCommandHandler>();
builder.Services.AddScoped<IEventHandler, InventoryManagement.Events.EventHandler>();

builder.Services.AddScoped<IWriteRepository<Event>, ProductEventWriteRepo>();
builder.Services.AddScoped<IWriteRepository<Product>, ProductWriteRepo>();

builder.Services.AddScoped<IReadRepository<Product>, ProductReadRepo>();
builder.Services.AddScoped<IReadRepository<Event>, ProductEventReadRepo>();

// Add RabbitMQ Publisher and Consumer services.
var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));


// Add hosted service for listening to RabbitMQ messages.
builder.Services.AddHostedService<ProductMessageListenerService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Run migrations if in production
if (builder.Environment.IsProduction())
{
    // TODO: Re-activate migrations when it is fixed
    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
