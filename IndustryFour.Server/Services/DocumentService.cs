using IndustryFour.Server.Models;
using IndustryFour.Server.Repositories;
using IndustryFour.Server.Retrieval;

namespace IndustryFour.Server.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentIndexService _documentIndexService;

    public DocumentService(IDocumentRepository documentRepository, IDocumentIndexService documentIndexService)
    {
        _documentRepository = documentRepository;
        _documentIndexService = documentIndexService;
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
        // TODO: Do a check for duplicate entries (content hash?)

        await _documentRepository.Add(document);

        // Retrieve the document so we get the category filled in
        document = await _documentRepository.GetById(document.Id);
        await _documentIndexService.Add(document);

        return document;
    }

    public async Task<Document> Update(Document document)
    {
        await _documentRepository.Update(document);

        // TODO: Update the index
        //await _documentIndex.Update(document);

        return document;
    }

    public async Task<bool> Remove(Document document)
    {
        // Order reversed from Add()
        await _documentIndexService.Remove(document);
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