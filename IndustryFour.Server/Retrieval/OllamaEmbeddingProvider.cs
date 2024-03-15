using OllamaSharp;

namespace IndustryFour.Server.Retrieval;

public class OllamaEmbeddingProvider : IEmbeddingProvider
{
    private const string DefaultEmbeddingModelName = "nomic-embed-text";

    private readonly OllamaApiClient _ollama;

    public OllamaEmbeddingProvider()
        : this(DefaultEmbeddingModelName)
    {
    }

    public OllamaEmbeddingProvider(string modelName)
    {
        // set up the client
        var uri = new Uri("http://localhost:11434");
        _ollama = new OllamaApiClient(uri);

        // select a model which should be used for further operations
        _ollama.SelectedModel = modelName;
    }

    public async Task<IEnumerable<float[]>> EmbedChunks(IEnumerable<string> chunks)
    {
        List<float[]> embeddings = [];

        foreach (string chunk in chunks)
        {
            var embedding = await EmbedChunk(chunk);
            embeddings.Add(embedding);
        }

        return embeddings;
    }

    public async Task<float[]> EmbedChunk(string chunk)
    {
        var response = await _ollama.GenerateEmbeddings(chunk);
        var embedding = response.Embedding.Select(float.CreateTruncating).ToArray();
        return embedding;
    }
}