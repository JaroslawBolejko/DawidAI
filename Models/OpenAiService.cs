namespace BlazorChatAI.Models
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<string> GetAnswerAsync(string userPrompt)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are helpfull assistent!" },
                    new { role = "user", content = userPrompt },
                },
                max_tokens = 100,
                temperature = 0.7,
            };

            using var response = await _httpClient.PostAsJsonAsync("https://openai.com/v1/chat/completions", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return "Sorry, ther is no answer from AI";
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
