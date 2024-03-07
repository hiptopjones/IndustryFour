using IndustryFour.Server.Interfaces;
using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<IEnumerable<Document>> GetAll()
    {
        return await _documentRepository.GetAll();
    }

    public async Task<Document> GetById(int id)
    {
        return await _documentRepository.GetById(id);
    }

    public async Task<Document> Add(Document document)
    {
        if (_documentRepository.Search(d => d.Title == document.Title).Result.Any())
        {
            return null;
        }

        await _documentRepository.Add(document);
        return document;
    }

    public async Task<Document> Update(Document document)
    {
        if (_documentRepository.Search(d => d.Title == document.Title && d.Id != document.Id).Result.Any())
        {
            return null;
        }

        await _documentRepository.Update(document);
        return document;
    }

    public async Task<bool> Remove(Document document)
    {
        await _documentRepository.Remove(document);
        return true;
    }

    public async Task<IEnumerable<Document>> GetDocumentsByCategory(int categoryId)
    {
        return await _documentRepository.GetDocumentsByCategory(categoryId);
    }

    public async Task<IEnumerable<Document>> Search(string documentTitle)
    {
        return await _documentRepository.Search(c => c.Title.Contains(documentTitle));
    }

    public async Task<IEnumerable<Document>> SearchDocumentsWithCategory(string searchedValue)
    {
        return await _documentRepository.SearchDocumentWithCategory(searchedValue);
    }

    public void Dispose()
    {
        _documentRepository?.Dispose();
    }
}