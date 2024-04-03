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
                    Embedding = new Vector(embedding),
                    DocumentId = document.Id
                };

				_logger.LogInformation($"Add chunk");
				await _chunkService.Add(chunk);

                chunks.Add(chunk);
            }

            // Add links to the next and previous chunks, so they can easily be pulled in if desired
            for (int i = 0; i < chunks.Count; i++)
            {
                if (i > 0)
                {
                    chunks[i].PreviousChunkId = chunks[i - 1].Id;
                }

                if (i < chunks.Count - 1)
                {
                    chunks[i].NextChunkId = chunks[i + 1].Id;
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

            for (int i = chunkMatches.Count - 1; i >= 0; i--)
            {
                var chunkMatch = chunkMatches[i];

                if (chunkMatch.Chunk.NextChunkId != 0)
                {
                    Chunk nextChunk = await _chunkService.GetById(chunkMatch.Chunk.NextChunkId);
                    chunkMatches.Insert(i + 1, new ChunkMatch { Chunk = nextChunk });
                }

                if (chunkMatch.Chunk.PreviousChunkId != 0)
                {
                    Chunk previousChunk = await _chunkService.GetById(chunkMatch.Chunk.PreviousChunkId);
                    chunkMatches.Insert(i, new ChunkMatch { Chunk = previousChunk });
                }
            }

            return chunkMatches;
        }
    }
}
