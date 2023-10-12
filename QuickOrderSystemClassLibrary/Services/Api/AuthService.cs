using System.Text;
using System.Text.Json;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class AuthService
    {
        private readonly string _baseApiUrl;
        private readonly HttpClient _httpClient;

        public AuthService(string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl;
            _httpClient = new HttpClient();
        }

        public async Task<QuickOrderSystemClassLibrary.User?> RegisterAsync(string username, string password)
        {
            var request = new QuickOrderSystemClassLibrary.UserDto { Username = username, Password = password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseApiUrl}/auth/register", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QuickOrderSystemClassLibrary.User>(jsonResponse);
        }

        public async Task<int?> LoginAsync(string username, string password)
        {
            var request = new QuickOrderSystemClassLibrary.UserDto { Username = username, Password = password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseApiUrl}/auth/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Den Inhalt der Antwort lesen (das sollte die UserId sein)
                var content = await response.Content.ReadAsStringAsync();
                if (int.TryParse(content, out int userId))
                {
                    return userId;
                }
                return null;  // Falls das Parsen nicht erfolgreich war
            }

            return null; // Falls die Antwort nicht erfolgreich war
        }
    }
}
