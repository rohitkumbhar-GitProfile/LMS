using LMS.Services.DTOs;
using LMS.Services.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LMS.Services.Services
{
    public class OpenAIAgent: IAIClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAIAgent(IOptions<OpenAIOptions> options)
        {
            _apiKey = options.Value.ApiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetSyllabusAsync(string courseName)
        {
            var prompt = courseName;
            var requestBody = new
            {
                model = "gpt-4o-mini-2024-07-18", // or "gpt-4" if your key supports it
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"OpenAI API error: {error}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            var result = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return result ?? "No syllabus found.";
        }
    }
}
