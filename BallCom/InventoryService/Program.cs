using InventoryService.Database;
using InventoryService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using InventoryService.Domain;
using InventoryService.EventHandlers;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using InventoryService.Services.RabbitMQ;
using RabbitMQ.Client;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<InventoryDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IReadRepository<Inventory>, InventoryRepo>();
builder.Services.AddScoped<IWriteRepository<InventoryBaseEvent>, InventoryEventRepo>();
builder.Services.AddScoped<IInventoryEventHandler, InventoryEventHandler>();
builder.Services.AddScoped<IProductEventHandler, ProductEventHandler>();

// Add RabbitMQ Publisher and Consumer services.
var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));


// Add hosted service for listening to RabbitMQ messages.
builder.Services.AddHostedService<InventoryMessageListenerService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Inventory API microservice Ball.com",
            Description = "An ASP.NET Core Web API for managing inventory for Ball.com",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Ball.com Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Ball.com License",
                Url = new Uri("https://example.com/license")
            }
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    });

// Run migrations if in production
if (builder.Environment.IsProduction())
{
    // TODO: Re-activate migrations when it is fixed
    // using var scope = builder.Services.BuildServiceProvider().CreateScope();
    // var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    // dbContext.Database.Migrate();
}

var app = builder.Build();

// Add swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();