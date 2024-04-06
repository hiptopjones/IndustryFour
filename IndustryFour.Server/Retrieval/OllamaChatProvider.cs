using OllamaSharp;
using System.Text;

namespace IndustryFour.Server.Retrieval
{
    public class OllamaChatProvider : IChatProvider
    {
        private const string DefaultEmbeddingModelName = "llama2";

        private readonly OllamaApiClient _ollama;

        public OllamaChatProvider()
            : this(DefaultEmbeddingModelName)
        {
        }

        public OllamaChatProvider(string modelName)
        {
            // set up the client
            var uri = new Uri("http://localhost:11434");
            _ollama = new OllamaApiClient(uri);

            // select a model which should be used for further operations
            _ollama.SelectedModel = modelName;
        }

        public async Task<string> Chat(string prompt)
        {
            StringBuilder responseBuilder = new StringBuilder();
            var chat = _ollama.Chat(stream => responseBuilder.Append(stream.Message?.Content ?? ""));
            var result = await chat.Send(prompt);
            return responseBuilder.ToString();
        }
    }
}
