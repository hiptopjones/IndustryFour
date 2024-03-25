using AutoMapper;
using IndustryFour.Server.Retrieval;
using IndustryFour.Server.Services;
using IndustryFour.Shared.Dtos.Chat;
using IndustryFour.Shared.Dtos.Chunk;
using IndustryFour.Shared.Dtos.Document;
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
		private readonly IMapper _mapper;

		public ChatController(ILogger<ChatController> logger, IDocumentIndexService index, IChatProvider chat, IMapper mapper)
        {
            _logger = logger;
            _index = index;
            _chat = chat;
            _mapper = mapper;
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
            response.Chunks = _mapper.Map<List<ChunkResultDto>>(chunks);
			response.SimilaritySearchDuration = stopwatch.Elapsed;

            var contextOnlyLimiter = request.UseContextOnly ? "If the answer is not in the provided context then just say that you don't know, don't try to make up an answer." : "";
            var conciseResponseLimiter = request.UseConciseResponse ? "Keep the answers short and to the point, unless asked to expand.\n" : "";
            var relevantChunks = string.Join("\n\n", chunks.Select(x => x.Content));

            var prompt = $"""
                You are a helpful assistant.
                Use the following pieces of context to answer the question at the end.
                {contextOnlyLimiter}{conciseResponseLimiter}
                
                {relevantChunks}

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
