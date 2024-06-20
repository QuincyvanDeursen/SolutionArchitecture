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
        public InventoryServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> GetInventoryAsync(List<ProductStockDto> productsFromOrder)
        {
            var json = JsonConvert.SerializeObject(productsFromOrder);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://host.docker.internal:3002/api/Product/checkstock", data);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(content);
        }
    }
}
