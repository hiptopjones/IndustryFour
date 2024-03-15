using IndustryFour.Server.Models;

namespace IndustryFour.Server.Retrieval
{
    public interface IDocumentIndex
    {
        Task Add(Document document);
        Task Update(Document document);
        Task Remove(Document document);

        Task<IEnumerable<string>> SimilaritySearch(string text, int k);
    }
}
