using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace IndustryFour.Server.Retrieval
{
    public class OpenAiChatProvider : IChatProvider
    {
        private readonly OpenAIClient _client;

        public OpenAiChatProvider()
        {
            // set up the client
            _client = new OpenAIClient();
        }

        public async Task<string> Chat(string prompt)
        {

            var chatRequest = new ChatRequest(new[] { new Message(Role.User, prompt) }, Model.GPT4);
            var response = await _client.ChatEndpoint.GetCompletionAsync(chatRequest);
            var choice = response.FirstChoice;

            return choice.Message;
        }
    }
}
