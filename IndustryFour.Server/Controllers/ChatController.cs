using IndustryFour.Server.Retrieval;
using Microsoft.AspNetCore.Mvc;

namespace GenTools.Server.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        private readonly IDocumentIndex _index;
        private readonly IChatProvider _chat;

        public ChatController(ILogger<ChatController> logger, IDocumentIndex index, IChatProvider chat)
        {
            _logger = logger;
            _index = index;
            _chat = chat;
        } 

        [HttpGet("question")]
        public async Task<IActionResult> AskQuestion(string question)
        {
            var chunks = await _index.SimilaritySearch(question, 3);

            var prompt = $"""
                Use the following pieces of context to answer the question at the end.
                If the answer is not in context then just say that you don't know, don't try to make up an answer.
                Keep the answer as short as possible.

                {string.Join("\n\n", chunks)}

                Question: {question}
                Helpful Answer:
                """;
            Console.WriteLine($"Prompt: {prompt}");

            var answer = await _chat.Chat(prompt);

            return Ok(answer);
        }
    }
}
