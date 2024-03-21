using IndustryFour.Server.Models;
using IndustryFour.Server.Services;

namespace IndustryFour.Server.Retrieval
{
    public class DocumentIndex : IDocumentIndex
    {
        private readonly ILoggerManager _logger;
        private readonly HttpClient _httpClient;
        private readonly IVectorStore _vectorStore;
        private readonly ITextSplitter _textSplitter;
        private readonly IEmbeddingProvider _embeddingProvider;

        public DocumentIndex(ILoggerManager logger, HttpClient httpClient, IVectorStore vectorStore, ITextSplitter textSplitter, IEmbeddingProvider embeddingProvider)
        {
            _logger = logger;
            _httpClient = httpClient;
            _vectorStore = vectorStore;
            _textSplitter = textSplitter;
            _embeddingProvider = embeddingProvider;
        }

        public async Task Add(Document document)
        {
            List<string> ids = [];
            List<float[]> embeddings = [];
            List<Dictionary<string, object>> metadatas = [];

            string content = await _httpClient.GetStringAsync(document.ContentUrl);
            var chunks = await _textSplitter.Split(content);

            int chunkIndex = 0;

            foreach (var chunk in chunks)
            {
                _logger.LogInfo($"Process chunk: {chunkIndex++} (length: {chunk.Length})");

                _logger.LogInfo($"Setting chunk ID");
                string id = Guid.NewGuid().ToString();
                ids.Add(id);

                _logger.LogInfo($"Embedding chunk");
                var embedding = await _embeddingProvider.EmbedChunk(chunk);
                embeddings.Add(embedding);

                _logger.LogInfo($"Setting chunk metadata");
                // TODO: Add some additional metadata about curation and entity recognition/editing
                var metadata = new Dictionary<string, object>
                {
                    { "DocumentId", document.Id },
                    { "ChunkId", id },
                    { "DocumentType", document.Category.Name },
                    { "DocumentAuthor", document.Author }
                };
                metadatas.Add(metadata);
            }

            _logger.LogInfo($"Store {ids.Count} chunk vectors");

            await _vectorStore.StoreVectors(ids, embeddings, metadatas, chunks);
        }

        public Task Remove(Document document)
        {
            throw new NotImplementedException();
        }

        public Task Update(Document document)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> SimilaritySearch(string text, int k)
        {
            float[] embedding = await _embeddingProvider.EmbedChunk(text);
            var relevantChunks = await _vectorStore.QueryVectors(embedding, k);
            return relevantChunks;
        }
    }
}
