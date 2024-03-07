using IndustryFour.Server.Models;

namespace IndustryFour.Server.Interfaces;

public interface IDocumentService : IDisposable
{
    Task<IEnumerable<Document>> GetAll();
    Task<Document> GetById(int id);
    Task<Document> Add(Document document);
    Task<Document> Update(Document document);
    Task<bool> Remove(Document document);
    Task<IEnumerable<Document>> GetDocumentsByCategory(int categoryId);
    Task<IEnumerable<Document>> Search(string documentTitle);
    Task<IEnumerable<Document>> SearchDocumentsWithCategory(string categoryName);
}
