using System.Text;
using Newtonsoft.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class QrCodeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _userId;

        public QrCodeService(string baseUrl, int userId)
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

        public async Task<List<QrCode>?> GetAllAsyncTask()
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/qrCode");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<QrCode>>(content);
            }
            return null;
        }

        public async Task<QrCode?> GetByIdAsyncTask(int id)
        {
            AddUserIdHeader();

            var response = await _httpClient.GetAsync($"{_baseUrl}/qrCode/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QrCode>(content);
            }
            return null;
        }

        public async Task CreateAsyncTask(QrCode qrCode)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(qrCode), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/qrCode", content);
        }

        public async Task UpdateAsyncTask(int id, QrCode qrCode)
        {
            AddUserIdHeader();

            var content = new StringContent(JsonConvert.SerializeObject(qrCode), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/qrCode/{id}", content);
        }

        public async Task DeleteAsyncTask(int id)
        {
            AddUserIdHeader();

            await _httpClient.DeleteAsync($"{_baseUrl}/qrCode/{id}");
        }
    }
}
