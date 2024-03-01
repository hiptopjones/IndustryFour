using IndustryFour.Server.LangChain;
using LangChain.Databases;
using LangChain.Docstore;
using LangChain.Providers;
using LangChain.Splitters.Text;
using LangChain.VectorStores;

namespace IndustryFour.Server.Services
{
    public class EmbeddingsService
    {
        private readonly IEmbeddingModel _embeddings;
        private readonly IChatModel _chat;

        private string VectorsFilePath { get; set; } = @"C:\temp\vectors.db";
        private string VectorsTableName { get; set; } = "vectors";

        public EmbeddingsService()
        {
            // https://levelup.gitconnected.com/updated-embeddings-in-ollama-0-1-26-cbdb5adc2ede
            _embeddings = new OllamaLanguageModelEmbeddings("nomic-embed-text");
            _chat = new OllamaLanguageModelInstruction("mistral");
        }

        public async Task ProcessTranscripts(string directoryPath)
        {
            List<Document> documents = new List<Document>();

            foreach (string documentFilePath in Directory.GetFiles(directoryPath))
            {
                string content = File.ReadAllText(documentFilePath);
                Document document = new Document(content, new Dictionary<string, object> { { "path", documentFilePath } });
                // TODO: Add some additional metadata about curation and entity recognition/editing

                documents.Add(document);
            }

            await SQLiteVectorStore.CreateIndexFromDocuments(
                embeddings: _embeddings,
                documents: documents,
                filename: VectorsFilePath,
                tableName: VectorsTableName,
                textSplitter: new RecursiveCharacterTextSplitter(
                    chunkSize: 1000,
                    chunkOverlap: 300));
        }

        public async Task ProcessDiscordLogs(string directoryPath)
        {
            List<Document> documents = new List<Document>();

            foreach (string documentFilePath in Directory.GetFiles(directoryPath))
            {
                string content = File.ReadAllText(documentFilePath);
                Document document = new Document(content, new Dictionary<string, object> { { "path", documentFilePath } });
                // TODO: Add some additional metadata about curation and entity recognition/editing

                documents.Add(document);
            }

            await SQLiteVectorStore.CreateIndexFromDocuments(
                embeddings: _embeddings,
                documents: documents,
                filename: VectorsFilePath,
                tableName: VectorsTableName,
                textSplitter: new DiscordLogSplitter());
        }

        public async Task<string> GetAnswer(string question)
        {
            var database = new SQLiteVectorStore(
                filename: VectorsFilePath,
                tableName: VectorsTableName,
                embeddings: _embeddings);

            var similarDocuments = await database.GetSimilarDocuments(question, amount: 5);

            var prompt = $"""
                Use the following pieces of context to answer the question at the end.
                If the answer is not in context then just say that you don't know, don't try to make up an answer.
                Keep the answer as short as possible.

                {similarDocuments.AsString()}

                Question: {question}
                Helpful Answer:
                """;

            Console.WriteLine($"Prompt: {prompt}");

            var answer = await _chat.GenerateAsync(prompt, cancellationToken: CancellationToken.None).ConfigureAwait(false);

            Console.WriteLine($"Answer: {answer}");
            Console.WriteLine();

            return answer;
        }
    }
}
