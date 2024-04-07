using IndustryFour.Server.Models;
using IndustryFour.Server.Retrieval;
using Pgvector;

namespace IndustryFour.Server.Services
{
	public class DocumentIndexService : IDocumentIndexService
    {
        private readonly ILogger<DocumentIndexService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IChunkService _chunkService;
        private readonly ITextSplitter _textSplitter;
        private readonly IEmbeddingProvider _embeddingProvider;

        public DocumentIndexService(
            ILogger<DocumentIndexService> logger,
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

            IEnumerable<string> textChunks;
            if (document.SourceUrl.Contains("linkedin.com"))
            {
                // No chunking on LinkedIn posts
                // TODO: Control this a more formal way
                textChunks = [content];
            }
            else
            {
                textChunks = await _textSplitter.Split(content);
            }

            int chunkIndex = 0;
            List<Chunk> chunks = new List<Chunk>();

            foreach (var textChunk in textChunks)
            {
                _logger.LogInformation($"Process chunk: {chunkIndex++} (length: {textChunk.Length})");

                _logger.LogInformation($"Embedding chunk");
                var embedding = await _embeddingProvider.EmbedChunk(textChunk);

                var chunk = new Chunk
                {
                    Content = textChunk,
                    EmbeddedText = textChunk,
                    Embedding = new Vector(embedding),
                    DocumentId = document.Id
                };

				_logger.LogInformation($"Add chunk");
				await _chunkService.Add(chunk);

                chunks.Add(chunk);
            }

            // Add the next and previous chunks to this chunk's content
            for (int i = 0; i < chunks.Count; i++)
            {
                if (i > 0)
                {
                    chunks[i].Content = chunks[i - 1].EmbeddedText + " " + chunks[i].Content;
                }

                if (i < chunks.Count - 1)
                {
                    chunks[i].Content = chunks[i].Content + " " + chunks[i + 1].EmbeddedText;
                }
            }

            // Persist the linkage changes
            foreach (var chunk in chunks)
            {
                await _chunkService.Update(chunk);
            }
        }

        public async Task Remove(Document document)
        {
            await _chunkService.RemoveByDocumentId(document.Id);
        }

        public Task Update(Document document)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChunkMatch>> SimilaritySearch(string text, int k)
		{
			float[] embedding = await _embeddingProvider.EmbedChunk(text);
            Vector vector = new Vector(embedding);

            var chunkMatches = (await _chunkService.GetByDistance(vector, k)).ToList();
            return chunkMatches;
        }
    }
}
