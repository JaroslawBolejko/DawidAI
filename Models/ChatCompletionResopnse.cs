namespace BlazorChatAI.Models
{
    public class ChatCompletionResopnse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public List<ChatCompletionChoice> Choices { get; set; }
        public ChatCompletionUsage Usage {get; set;}
    }
}
