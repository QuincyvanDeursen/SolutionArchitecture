﻿using CustomerService.Dto;
using CustomerService.Services.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using System.Text.Json;

namespace CustomerService.Services.CronJob
{
    public class CronJobService : IHostedService, IDisposable
    {
        private readonly string Url =
            "https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D";
        private readonly HttpClient client;
        private readonly ILogger<CronJobService> _logger;
        private readonly IHost host;
        Timer? timer;

        public CronJobService(HttpClient client, ILogger<CronJobService> logger, IHost host)
        {
            this.client = client;
            this._logger = logger;
            this.host = host;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var currentTime = DateTime.Now;
            var midnight = DateTime.Today.AddDays(1);
            var timeToMidnight = midnight - currentTime;
            
            // TODO: Remove starting timeout
            Thread.Sleep(10000);

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
                _logger.LogError($"Failed to retrieve customer data with status code: {response.StatusCode}");
                throw new Exception("Failed to retrieve customer data.");
            }

            _logger.LogInformation($"Received customers with status code: {response.StatusCode}");

            var newCustomers = new List<CustomerCreateDto>();
            var content = await response.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(content);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;

                var values = line.Split(',');
                newCustomers.Add(new CustomerCreateDto
                {
                    FirstName = values[1],
                    LastName = values[2],
                    PhoneNumber = values[3],
                    CompanyName = values[0],
                    Address = values[4]
                });
            }

            using var scope = host.Services.CreateScope();
            var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

            // Save the customers to the database
            var oldCustomers = await customerService.GetAll();

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
                await customerService.Create(customer);
            }

            _logger.LogInformation($"Added {count} new customers to the database.");   
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (timer == null) return Task.CompletedTask;
            timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (timer == null) return;
            timer.Dispose();
        }
    }
}