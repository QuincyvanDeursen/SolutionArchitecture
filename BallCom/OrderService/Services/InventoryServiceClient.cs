using Newtonsoft.Json;
using OrderService.Services.Interface;
using OrderService.DTO;
using System.Text;
using Shared.Models;
using Shared.Models.Inventory;

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

        public async Task<bool> CheckStockAsync(List<OrderItemDto> productsFromOrder)
        {
            try
            {
                var productList = productsFromOrder.Select(x => new CheckStock() {ProductId = x.ProductId, Quantity = x.Quantity}).ToList();
                var json = JsonConvert.SerializeObject(productList);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
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
