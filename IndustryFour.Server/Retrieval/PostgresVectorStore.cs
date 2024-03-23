using LangChain.Databases.Postgres;
using Newtonsoft.Json;

namespace IndustryFour.Server.Retrieval;

// Based on https://github.com/tryAGI/LangChain/blob/main/src/Databases/Postgres/src/PostgresVectorStore.cs
public class PostgresVectorStore : IVectorStore
{
    private const string DefaultSchema = "public";
    private const string DefaultCollectionName = "vectors";

    private readonly DistanceStrategy _distanceStrategy;
    private readonly string _collectionName;

    private readonly PostgresDbClient _postgresDbClient;

    public PostgresVectorStore(
        string connectionString,
        int vectorSize,
        string schema = DefaultSchema,
        string collectionName = DefaultCollectionName,
        DistanceStrategy distanceStrategy = DistanceStrategy.Cosine)
    {
        _postgresDbClient = new PostgresDbClient(connectionString, schema, vectorSize);

        _collectionName = collectionName;
        _distanceStrategy = distanceStrategy;
    }

    public async Task<IEnumerable<string>> QueryVectors(float[] embedding, int k)
    {
        await EnsureVectorsTableExists();

        var result = await _postgresDbClient.GetWithDistanceAsync(
            _collectionName,
            embedding,
            _distanceStrategy,
            limit: k);

        Console.WriteLine("QueryVectors() results:");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

        return result.Select(x => x.Item1.Content).ToList();
    }

    public async Task StoreVectors(
        IEnumerable<string> ids,
        IEnumerable<float[]> embeddings,
        IEnumerable<Dictionary<string, object>> metadatas,
        IEnumerable<string> documents)
    {
        await EnsureVectorsTableExists();

        // Needed to be able to index into them
        var idsArray = ids.ToArray();
        var embeddingsArray = embeddings.ToArray();
        var metadatasArray = metadatas.ToArray();
        var documentsArray = documents.ToArray();

        for (var i = 0; i < idsArray.Length; i++)
        {
            await _postgresDbClient.UpsertAsync(
                _collectionName,
                id: idsArray[i],
                documentsArray[i],
                metadatasArray[i],
                embeddingsArray[i],
                DateTime.UtcNow
            ).ConfigureAwait(false);
        }
    }

    private async Task EnsureVectorsTableExists()
    {
        if (!await _postgresDbClient.IsTableExistsAsync(_collectionName))
        {
            await _postgresDbClient.CreateEmbeddingTableAsync(_collectionName);
        }
    }
}
