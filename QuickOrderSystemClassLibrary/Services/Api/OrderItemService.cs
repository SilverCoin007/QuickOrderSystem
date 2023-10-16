using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class OrderItemService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _userId;

        public OrderItemService(string baseUrl, int userId)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
            _userId = userId;
        }

        private void AddUserIdHeader()
        {
            _httpClient.DefaultRequestHeaders.Remove("UserId");
            _httpClient.DefaultRequestHeaders.Add("UserId", _userId.ToString());
        }

        public async Task<List<OrderItem>?> GetAllByOrderIdAsyncTask(int orderId)
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/orderitem/byorder/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<OrderItem>>(content);
            }
            return null;
        }

        public async Task<OrderItem?> GetByIdAsyncTask(int id)
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/orderitem/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderItem>(content);
            }
            return null;
        }

        public async Task CreateAsyncTask(OrderItem orderItem)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(orderItem), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/orderitem", content);
        }

        public async Task UpdateAsyncTask(int id, OrderItem orderItem)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(orderItem), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/orderitem/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            AddUserIdHeader();

            await _httpClient.DeleteAsync($"{_baseUrl}/orderitem/{id}");
        }
    }
}
