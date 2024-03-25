using IndustryFour.Shared.Dtos.Chat;

namespace IndustryFour.Client.Pages
{
    internal class ChatMessage
    {
        public bool IsUserMessage { get; set; }
        public bool IsAssistanMessage => !IsUserMessage;

        public ChatRequestDto UserMessage { get; set; }
        public ChatResponseDto AssistantMessage { get; set; }

        public string ErrorMessage { get; set; }
    } 
}