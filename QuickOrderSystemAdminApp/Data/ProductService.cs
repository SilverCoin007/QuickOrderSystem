using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemAdminApp.Data
{
    internal class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(content);
            }
            return null;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(content);
            }
            return null;
        }

        public async Task CreateAsync(Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/products", content);
        }

        public async Task UpdateAsync(int id, Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/products/{id}", content);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/products/{id}");
        }
    }
}
