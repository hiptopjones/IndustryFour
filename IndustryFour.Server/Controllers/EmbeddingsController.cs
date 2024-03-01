using IndustryFour.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndustryFour.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbeddingsController : ControllerBase
    {
        private readonly ILogger<EmbeddingsController> _logger;

        private readonly EmbeddingsService _embeddings;

        public EmbeddingsController(ILogger<EmbeddingsController> logger, EmbeddingsService embeddings)
        {
            _logger = logger;
            _embeddings = embeddings;
        }

        [HttpGet("answer")]
        public async Task<string> GetAnswer(string question)
        {
            return await _embeddings.GetAnswer(question);
        }

        [HttpPost("transcripts")]
        public async Task ProcessTranscripts(string directoryPath)
        {
            await _embeddings.ProcessTranscripts(directoryPath);
        }

        [HttpPost("discord")]
        public async Task ProcessDiscordLogs(string directoryPath)
        {
            await _embeddings.ProcessDiscordLogs(directoryPath);
        }
    }
}
