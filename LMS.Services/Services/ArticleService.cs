using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LMS.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ArticleService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public ArticleService(HttpClient httpClient, AppDbContext context, IConfiguration config)
    {
        _httpClient = httpClient;
        _context = context;
        _config = config;
    }

    public async Task<List<ArticleRecommendation>> GetArticlesForUserAsync(string userName)
    {
        var userId = (await _context.Users.FirstOrDefaultAsync(u => u.Username == userName)).Id;
        var user = await _context.UserProfile.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new List<ArticleRecommendation>();

        var prompt = $@"
Given the following user profile:

Job: {user.CurrentJob}
Education: {user.EducationLevel}
Field of Study: {user.FieldOfStudy}
Current Skills: {user.CurrentSkills}
Interested Skills: {user.InterestedSkills}
Passion: {user.Passion}
Goal: {user.Goal}

Suggest 10 interesting article recommendations based on users data, interests, etc. 
Return in this JSON format:

[
  {{
    ""title"": ""string"",
    ""description"": ""string"",
    ""link"": ""articleLink""
  }}
]

Respond strictly in JSON format as an array of objects with: title, description, and link.
";

        var requestBody = new
        {
            model = "gpt-4o-mini-2024-07-18",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var requestJson = JsonSerializer.Serialize(requestBody);
        var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _config["OpenAI:ApiKey"]);

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var jsonDoc = JsonDocument.Parse(responseJson);
        var content = jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").ToString();

        if (content.StartsWith("```json"))
        {
            content = content.Replace("```json", "").Trim();
        }
        if (content.StartsWith("```"))
        {
            content = content.Replace("```", "").Trim();
        }
        if (content.EndsWith("```"))
        {
            content = content.Substring(0, content.LastIndexOf("```")).Trim();
        }

        return JsonSerializer.Deserialize<List<ArticleRecommendation>>(content);
    }
}
