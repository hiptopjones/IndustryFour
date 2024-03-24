using IndustryFour.Server.Retrieval;
using IndustryFour.Server.Services;
using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GenTools.Server.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        private readonly IDocumentIndexService _index;
        private readonly IChatProvider _chat;

        public ChatController(ILogger<ChatController> logger, IDocumentIndexService index, IChatProvider chat)
        {
            _logger = logger;
            _index = index;
            _chat = chat;
        } 

        [HttpPost]
        public async Task<IActionResult> AskQuestion(ChatRequestDto request)
        {
            ChatResponseDto response = new ChatResponseDto
            {
                Request = request
            };

            Stopwatch stopwatch;

            stopwatch = Stopwatch.StartNew();
            var chunks = await _index.SimilaritySearch(request.Question, request.MaxSearchResults != 0 ? request.MaxSearchResults : 1);
            response.Chunks = new List<string>(chunks);
            response.SimilaritySearchDuration = stopwatch.Elapsed;

            var prompt = $"""
                Use the following pieces of context to answer the question at the end.
                If the answer is not in context then just say that you don't know, don't try to make up an answer.
                Keep the answers short and to the point, unless asked to expand.

                {string.Join("\n\n", chunks)}

                Question: {request.Question}
                Helpful Answer:
                """;
            response.Prompt = prompt;

            stopwatch = Stopwatch.StartNew();
            var answer = await _chat.Chat(prompt);
            response.Answer = answer;
            response.ChatProviderDuration = stopwatch.Elapsed;

            return Ok(response);
        }
    }
}
