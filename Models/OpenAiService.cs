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
            if (userPrompt.Equals("Dawid jesteś?", StringComparison.OrdinalIgnoreCase)
                || userPrompt.Equals("Dawid jestes?", StringComparison.OrdinalIgnoreCase)
                || userPrompt.Equals("Dawid...", StringComparison.OrdinalIgnoreCase)
                || userPrompt.Equals("Daaawiid", StringComparison.OrdinalIgnoreCase))
            {
                return "Noo, jestem! Co tam chcesz?";
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = userPrompt },
                },
                max_tokens = 200,
                temperature = 0.7,
            };

            using var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                return "Sorry, there is no answer from AI";
            }

            ChatCompletionResopnse jsonResopnse = await response.Content.ReadFromJsonAsync<ChatCompletionResopnse>();        
            string answer = jsonResopnse?.Choices?.FirstOrDefault()?.Message?.Content?.Trim();
            
            return string.IsNullOrWhiteSpace(answer) ? "Got no answer" : answer;
        }
    }
}
