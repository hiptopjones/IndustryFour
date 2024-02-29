using Azure.AI.OpenAI;
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

        private string DocumentFilePath { get; set; } = @"C:\temp\document.txt";
        private string VectorsFilePath { get; set; } = @"C:\temp\vectors.db";
        private string VectorsTableName { get; set; } = "vectors";

        public EmbeddingsService()
        {
            // https://levelup.gitconnected.com/updated-embeddings-in-ollama-0-1-26-cbdb5adc2ede
            _embeddings = new OllamaLanguageModelEmbeddings("nomic-embed-text");
            _chat = new OllamaLanguageModelInstruction("mistral");
        }

        public async Task InitializeService()
        {
            if (!File.Exists(VectorsFilePath))
            {
                string content = File.ReadAllText(DocumentFilePath);
                Document document = new Document(content, new Dictionary<string, object> { { "path", DocumentFilePath } });
                Document[] documents = [document];

                await SQLiteVectorStore.CreateIndexFromDocuments(
                    embeddings: _embeddings,
                    documents: documents,
                    filename: VectorsFilePath,
                    tableName: VectorsTableName,
                    textSplitter: new RecursiveCharacterTextSplitter(
                        chunkSize: 200,
                        chunkOverlap: 50));
            }

            Console.WriteLine($"Embeddings usage: {_embeddings.Usage}");
        }

        public async Task<string> GetAnswer(string question)
        {
            var database = new SQLiteVectorStore(
                filename: VectorsFilePath,
                tableName: VectorsTableName,
                embeddings: _embeddings);

            var similarDocuments = await database.GetSimilarDocuments(question, amount: 5);

            var answer = await _chat.GenerateAsync(
                $"""
                Use the following pieces of context to answer the question at the end.
                If the answer is not in context then just say that you don't know, don't try to make up an answer.
                Keep the answer as short as possible.

                {similarDocuments.AsString()}

                Question: {question}
                Helpful Answer:
                """, cancellationToken: CancellationToken.None).ConfigureAwait(false);

            Console.WriteLine($"LLM answer: {_chat.Usage}");
            Console.WriteLine($"Embeddings usage: {_embeddings.Usage}");

            return answer;
        }
    }
}
