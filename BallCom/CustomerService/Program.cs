using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using CustomerService.Database;
using CustomerService.Repository;
using CustomerService.Repository.Interfaces;
using CustomerService.Services.RabbitMQ;
using Shared.MessageBroker;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Consumer;
using Shared.MessageBroker.Consumer.Interfaces;
using Shared.MessageBroker.Publisher;
using Shared.MessageBroker.Publisher.Interfaces;
using CustomerService.Services.Interfaces;
using CustomerService.Services;
using CustomerService.Services.CronJob;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<CustomerDbContext>(
    options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService.Services.CustomerService>();

builder.Services.AddHostedService<CronJobService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Customer API microservice Ball.com",
            Description = "An ASP.NET Core Web API for managing customers for Ball.com",
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

builder.Services.AddHttpClient();

// Run migrations if in production
if (builder.Environment.IsProduction())
{
    // Add RabbitMQ Publisher and Consumer services.
    var exchangeName = builder.Configuration.GetValue<string>("RabbitMQ:ExchangeName");
    var queueName = builder.Configuration.GetValue<string>("RabbitMQ:QueueName");

    builder.Services.AddSingleton<IConnectionProvider>(x => new RabbitMqConnectionProvider(builder.Configuration.GetValue<string>("RabbitMQ:Uri") ?? ""));
    builder.Services.AddSingleton<IMessagePublisher>(x => new RabbitMqMessagePublisher(x.GetService<IConnectionProvider>(), exchangeName));
    builder.Services.AddSingleton<IMessageConsumer>(x => new RabbitMqMessageConsumer(x.GetService<IConnectionProvider>(), exchangeName, queueName));

    // Add a hosted service for listening to RabbitMQ messages (consumer).
    builder.Services.AddHostedService<CustomerMessageListenerService>();

    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    dbContext.Database.Migrate();
}

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
