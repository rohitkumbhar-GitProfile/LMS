using LMS.Services.DTOs;
using LMS.Services.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Services
{
    public class GPT4MiniTTSClient : ITTSClient
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        public GPT4MiniTTSClient(IOptions<TextToSpeechOptions> options)
        {
            _apiKey = options.Value.ApiKey;
            _apiUrl = options.Value.ApiUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }
        public async Task<byte[]> ConvertTextToSpeechAsync(string text)
        {
            var requestBody = new
            {
                input = text,
                voice = "alloy", // Adjust based on actual API docs
                format = "mp3",
                model = "gpt-4o-mini-tts" // ✅ Correct lowercase 'model'
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"TTS API error: {error}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
