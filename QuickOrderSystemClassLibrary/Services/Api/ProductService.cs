using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<Product>?> GetAllAsyncTask()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(content);
            }
            return null;
        }

        public async Task<Product?> GetByIdAsyncTask(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(content);
            }
            return null;
        }

        public async Task CreateAsyncTask(Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/product", content);
        }

        public async Task UpdateAsyncTask(int id, Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/product/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/product/{id}");
        }
    }
}
