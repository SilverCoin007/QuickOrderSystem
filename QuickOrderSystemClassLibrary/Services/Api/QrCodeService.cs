﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemAdminApp.Data
{
    public class QrCodeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public QrCodeService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<QrCode>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/qrCode");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<QrCode>>(content);
            }
            return null;
        }

        public async Task<QrCode> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/qrCode/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QrCode>(content);
            }
            return null;
        }

        public async Task CreateAsync(QrCode qrCode)
        {
            var content = new StringContent(JsonConvert.SerializeObject(qrCode), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/qrCode", content);
        }

        public async Task UpdateAsync(int id, QrCode qrCode)
        {
            var content = new StringContent(JsonConvert.SerializeObject(qrCode), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/qrCode/{id}", content);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/qrCode/{id}");
        }
    }
}