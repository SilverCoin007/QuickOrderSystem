using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _userId;

        public ProductService(string baseUrl, int userId)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
            _userId = userId;
        }

        private void AddUserIdHeader()
        {
            _httpClient.DefaultRequestHeaders.Remove("UserId"); // Remove existing header to ensure we don't add it multiple times
            _httpClient.DefaultRequestHeaders.Add("UserId", _userId.ToString());
        }

        public async Task<List<Product>?> GetAllAsyncTask()
        {
            AddUserIdHeader();

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
            AddUserIdHeader();

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
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/product", content);
        }

        public async Task UpdateAsyncTask(int id, Product product)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/product/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            AddUserIdHeader();

            await _httpClient.DeleteAsync($"{_baseUrl}/product/{id}");
        }
    }
}
