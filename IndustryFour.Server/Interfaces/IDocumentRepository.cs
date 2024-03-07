using IndustryFour.Server.Models;

namespace IndustryFour.Server.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>, IDisposable
    {
        new Task<List<Document>> GetAll();
        new Task<Document> GetById(int id);
        Task<IEnumerable<Document>> GetDocumentsByCategory(int categoryId);
        Task<IEnumerable<Document>> SearchDocumentWithCategory(string searchedValue);
    }
}
