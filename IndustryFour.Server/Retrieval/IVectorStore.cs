namespace IndustryFour.Server.Retrieval
{
    public interface IVectorStore
    {
        Task StoreVectors(IEnumerable<string> ids, IEnumerable<float[]> embeddings, IEnumerable<Dictionary<string, object>> metadatas, IEnumerable<string> documents);
        Task<IEnumerable<string>> QueryVectors(float[] embedding, int k);
    }
}