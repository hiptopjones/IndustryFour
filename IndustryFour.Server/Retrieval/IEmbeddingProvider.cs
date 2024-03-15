namespace IndustryFour.Server.Retrieval
{
    public interface IEmbeddingProvider
    {
        Task<float[]> EmbedChunk(string chunk);
        Task<IEnumerable<float[]>> EmbedChunks(IEnumerable<string> chunks);
    }
}