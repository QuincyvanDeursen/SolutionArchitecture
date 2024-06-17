using OrderService.Domain;
using Newtonsoft.Json;
using OrderService.Services.Interface;

namespace OrderService.Services
{
    public class InventoryServiceClient : IInventoryServiceClient
    {
        private readonly HttpClient _httpClient;
        public InventoryServiceClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Inventory> GetInventoryAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/inventory/{productId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Inventory>(content);
        }
    }
}
