using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemAdminApp.Data
{
    internal class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OrderService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(content);
            }
            return null;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/order/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Order>(content);
            }
            return null;
        }

        public async Task CreateAsync(Order order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/order", content);
        }

        public async Task UpdateAsync(int id, Order order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/order/{id}", content);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/order/{id}");
        }
    }
}
