using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _userId;

        public CategoryService(string baseUrl, int userId)
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

        public async Task<List<Category>?> GetAllAsyncTask()
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/category");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Category>>(content);
            }
            return null;
        }

        public async Task<Category?> GetByIdAsyncTask(int id)
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/category/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Category>(content);
            }
            return null;
        }

        public async Task CreateAsyncTask(Category category)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/category", content);
        }

        public async Task UpdateAsyncTask(int id, Category category)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/category/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            AddUserIdHeader();

            await _httpClient.DeleteAsync($"{_baseUrl}/category/{id}");
        }
    }
}
