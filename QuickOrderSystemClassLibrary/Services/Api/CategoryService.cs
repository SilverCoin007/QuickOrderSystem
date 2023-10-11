using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CategoryService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<Category>?> GetAllAsyncTask()
        {
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
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/category", content);
        }

        public async Task UpdateAsyncTask(int id, Category category)
        {
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/category/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/category/{id}");
        }
    }
}
