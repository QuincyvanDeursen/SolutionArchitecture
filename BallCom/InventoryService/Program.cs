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


var exchangeName = builder.Configuration.GetSection("RabbitMQ:ExchangeName").Value;
var queueName = builder.Configuration.GetSection("RabbitMQ:QueueName").Value;

builder.Services.AddScoped<IReadRepository<Inventory>, InventoryRepo>();
builder.Services.AddScoped<IWriteRepository<InventoryBaseEvent>, InventoryEventRepo>();
builder.Services.AddScoped<IInventoryEventHandler, InventoryEventHandler>();

// Add RabbitMQ Publisher and Consumer services.
builder.Services.AddSingleton<IConnectionFactory>(x => new ConnectionFactory
{
    Uri = new Uri(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? "")
});
builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionFactory>(), exchangeName));
builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionFactory>(), exchangeName, queueName));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();