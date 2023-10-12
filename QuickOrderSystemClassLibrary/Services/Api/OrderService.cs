using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _userId;

        public OrderService(string baseUrl, int userId)
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

        public async Task<List<Order>?> GetAllAsyncTask()
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(content);
            }
            return null;
        }

        public async Task<Order?> GetByIdAsyncTask(int id)
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/order/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Order>(content);
            }
            return null;
        }

        public async Task CreateAsyncTask(Order order)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/order", content);
        }

        public async Task UpdateAsyncTask(int id, Order order)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/order/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            AddUserIdHeader();

            await _httpClient.DeleteAsync($"{_baseUrl}/order/{id}");
        }
    }
}
