using CustomerService.Domain;
using System;
using System.Text.Json;
using CustomerService.Repository.Interfaces;
using CustomerService.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.MessageBroker.Publisher.Interfaces;

namespace CustomerService.Services.CronJob
{
    public class CronJobService(HttpClient client, ILogger<CronJobService> logger, IHost host, Timer timer)
        : IHostedService, IDisposable
    {
        private readonly string Url =
            "https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var currentTime = DateTime.Now;
            var midnight = DateTime.Today.AddDays(1);
            var timeToMidnight = midnight - currentTime;

            // Create a timer that runs the background task
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2)); // Runs every 24 hours

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            //Get all customers from the external API
            var response = await client.GetAsync(Url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve customer data.");
            }

            logger.LogInformation($"Received customers with status code: {response.StatusCode}");

            var newCustomers = new List<Customer>();
            var content = await response.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(content);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;

                var values = line.Split(',');
                newCustomers.Add(new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = values[0],
                    LastName = values[1],
                    PhoneNumber = values[2],
                    CompanyName = values[3],
                    Address = values[4]
                });
            }

            using var scope = host.Services.CreateScope();
            var customerService = scope.ServiceProvider.GetRequiredService<ICustomerRepo>();
            var messageBroker = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();

            // Save the customers to the database
            var oldCustomers = await customerService.GetAllCustomers();

            // Check if the customer already exists in the database
            var count = 0;
            foreach (var customer in newCustomers)
            {
                if (oldCustomers.Any(c => c.PhoneNumber == customer.PhoneNumber))
                {
                    continue;
                }

                count++;

                // Save new customer to the database
                await customerService.AddCustomer(customer);

                // Publish the event to the message broker
                await messageBroker.PublishAsync(JsonSerializer.Serialize(customer), "customer.create");
            }

            logger.LogInformation($"Added {count} new customers to the database.");

            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer when the application is shutting down
            timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Dispose of the timer object
            timer.Dispose();
        }
    }
}