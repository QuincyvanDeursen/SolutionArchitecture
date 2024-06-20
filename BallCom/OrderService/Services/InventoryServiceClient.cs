using OrderService.Domain;
using Newtonsoft.Json;
using OrderService.Services.Interface;
using OrderService.DTO;
using System.Text;

namespace OrderService.Services
{
    public class InventoryServiceClient : IInventoryServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InventoryServiceClient> _logger;

        public InventoryServiceClient(HttpClient httpClient, ILogger<InventoryServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> GetInventoryAsync(List<ProductStockDto> productsFromOrder)
        {
            try
            {
                var json = JsonConvert.SerializeObject(productsFromOrder);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
            http://localhost:3002/checkstock
                var response = await _httpClient.PostAsync("http://host.docker.internal:3002/api/Product/checkstock", data);
                response.EnsureSuccessStatusCode(); // This ensures the HTTP status code is success (2xx range)

                var content = await response.Content.ReadAsStringAsync();
                bool checkStockResult = JsonConvert.DeserializeObject<bool>(content);

                return checkStockResult;
            }
            
            catch (Exception ex)
            {
                // Handle other exceptions specific to your application logic
                _logger.LogError($"Exception occurred: {ex.Message}");
                return false; 
            }
        }
    }
}
