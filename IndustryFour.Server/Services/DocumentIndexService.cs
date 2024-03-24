using IndustryFour.Server.Models;
using IndustryFour.Server.Retrieval;
using Pgvector;

namespace IndustryFour.Server.Services
{
	public class DocumentIndexService : IDocumentIndexService
    {
        private readonly ILoggerManager _logger;
        private readonly HttpClient _httpClient;
        private readonly IChunkService _chunkService;
        private readonly ITextSplitter _textSplitter;
        private readonly IEmbeddingProvider _embeddingProvider;

        public DocumentIndexService(
            ILoggerManager logger,
            HttpClient httpClient,
            IChunkService chunkService,
            ITextSplitter textSplitter,
            IEmbeddingProvider embeddingProvider)
        {
            _logger = logger;
            _httpClient = httpClient;
            _chunkService = chunkService;
            _textSplitter = textSplitter;
            _embeddingProvider = embeddingProvider;
        }

        public async Task Add(Document document)
        {
            string content = await _httpClient.GetStringAsync(document.ContentUrl);
            var textChunks = await _textSplitter.Split(content);

            int chunkIndex = 0;

            foreach (var textChunk in textChunks)
            {
                _logger.LogInfo($"Process chunk: {chunkIndex++} (length: {textChunk.Length})");

                _logger.LogInfo($"Embedding chunk");
                var embedding = await _embeddingProvider.EmbedChunk(textChunk);

                var chunk = new Chunk
                {
                    Content = textChunk,
                    Embedding = new Vector(embedding),
                    DocumentId = document.Id
                };

				_logger.LogInfo($"Add chunk");
				await _chunkService.Add(chunk);
            }
        }

        public Task Remove(Document document)
        {
            throw new NotImplementedException();
        }

        public Task Update(Document document)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Chunk>> SimilaritySearch(string text, int k)
		{
			float[] embedding = await _embeddingProvider.EmbedChunk(text);
            Vector vector = new Vector(embedding);

            var relevantChunks = await _chunkService.GetChunksByDistance(vector, k);
            return relevantChunks;
        }
    }
}
