using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services
{
    public interface IChatService
    {
        Task<ChatResponse> AskQuestion(ChatRequest request);
    }
}
