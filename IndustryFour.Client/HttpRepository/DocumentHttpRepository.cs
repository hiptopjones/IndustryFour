using IndustryFour.Shared.Dtos.Document;
using System.Net.Http.Json;

namespace IndustryFour.Client.HttpRepository
{
    public class DocumentHttpRepository : IDocumentHttpRepository
    {
        private readonly HttpClient _client;
        
        public DocumentHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<DocumentResultDto>> GetAll()
        {
            var documents = await _client.GetFromJsonAsync<List<DocumentResultDto>>("documents");
            return documents;
        }
    }
}
