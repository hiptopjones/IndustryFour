using ChromaDBSharp.Client;
using Newtonsoft.Json;

namespace IndustryFour.Server.Retrieval
{
    public class ChromaDbVectorStore : IVectorStore
    {
        private readonly ChromaDBClient _client;
        private readonly string _collectionName;

        public ChromaDbVectorStore(string collectionName = "vectors")
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8000/");

            _client = new(httpClient);
            _collectionName = collectionName;

            _client.CreateCollection(_collectionName, createOrGet: true);
        }

        public async Task<IEnumerable<string>> QueryVectors(float[] embedding, int k)
        {
            ICollectionClient collectionClient = _client.GetCollection(_collectionName);
            Console.WriteLine($"Collection document count: {collectionClient.Count()}");

            var result = await collectionClient.QueryAsync(
                queryEmbeddings: [embedding],
                numberOfResults: k);

            Console.WriteLine("QueryVectors() results:");
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            return result.Documents.First();
        }

        public async Task StoreVectors(IEnumerable<string> ids, IEnumerable<float[]> embeddings, IEnumerable<Dictionary<string, object>> metadatas, IEnumerable<string> documents)
        {
            ICollectionClient collectionClient = _client.GetCollection(_collectionName);
            await collectionClient.AddAsync(ids, embeddings, metadatas, documents);
        }

    }
}