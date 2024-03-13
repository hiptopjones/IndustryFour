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

        public async Task<List<DocumentResultDto>> GetDocuments()
        {
            var documents = await _client.GetFromJsonAsync<List<DocumentResultDto>>("documents");
            return documents;
        }

		public async Task<DocumentResultDto> GetDocument(int id)
		{
			var document = await _client.GetFromJsonAsync<DocumentResultDto>($"documents/{id}");
			return document;
		}

        public async Task CreateDocument(DocumentAddDto document)
        {
            await _client.PostAsJsonAsync("documents", document);
        }

		public async Task<string> UploadDocumentFile(MultipartFormDataContent content)
		{
            var postResult = await _client.PostAsync("upload", content);
            var contentUrlPath = await postResult.Content.ReadAsStringAsync();
            var contentUrl = Path.Combine("https://localhost:5101", contentUrlPath);

            return contentUrl;
		}
	}
}
