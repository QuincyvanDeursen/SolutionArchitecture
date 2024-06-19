using CustomerService.Domain;
using System;

namespace CustomerService.Services.CronJob
{
    public class CronJobService : IHostedService, IDisposable
    {

        private Timer _timer;
        private readonly HttpClient _client;
        private readonly string Url = "https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D";

        public CronJobService(HttpClient client)
        {
            _client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var currentTime = DateTime.Now;
            var midnight = DateTime.Today.AddDays(1);
            var timeToMidnight = midnight - currentTime;

            // Create a timer that runs the background task
            _timer = new Timer(DoWork, null, timeToMidnight, TimeSpan.FromDays(1)); // Runs every 24 hours

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            // Your background task logic goes here
            // ...
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer when the application is shutting down
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Dispose of the timer object
            _timer?.Dispose();
        }

        private async Task GetAllCustomers()
        {
            var response = await _client.GetAsync(Url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve customer data.");
            }
            var customers = new List<Customer>();
            var content = await response.Content.ReadAsStringAsync();
            // streamreader
            using (var reader = new StreamReader(content))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;
                    var values = line.Split(',');
                    customers.Add(new Customer
                    {
                        Id = Guid.NewGuid(),
                        FirstName = values[0],
                        LastName = values[1],
                        PhoneNumber = values[2],
                        CompanyName = values[3],
                        Address = values[4]
                    });
                }
            }
        }
    }
}
