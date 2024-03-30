using IndustryFour.Shared.Dtos.Document;
using System.Net.Http.Json;

namespace IndustryFour.Client.HttpRepository
{
    public class DocumentHttpRepository : IDocumentHttpRepository
    {
        private readonly HttpClient _client;
        private readonly ApiConfiguration _apiConfiguration;

        public DocumentHttpRepository(HttpClient client, ApiConfiguration apiConfiguration)
        {
            _client = client;
            _apiConfiguration = apiConfiguration;
        }

        public async Task<List<DocumentResultDto>> GetAll()
        {
            var documents = await _client.GetFromJsonAsync<List<DocumentResultDto>>("documents");
            return documents;
        }

		public async Task<DocumentResultDto> GetById(int id)
		{
			var document = await _client.GetFromJsonAsync<DocumentResultDto>($"documents/{id}");
			return document;
		}

        public async Task Create(DocumentAddDto document)
        {
            await _client.PostAsJsonAsync("documents", document);
        }

		public async Task<string> UploadFile(MultipartFormDataContent content)
		{
            var postResult = await _client.PostAsync("upload", content);
            var contentUrlPath = await postResult.Content.ReadAsStringAsync();
            var contentUrl = Path.Combine(_apiConfiguration.BaseAddress, contentUrlPath).Replace("\\", "/");

            return contentUrl;
		}

        public async Task Update(DocumentEditDto document) =>
            await _client.PutAsJsonAsync($"documents/{document.Id}", document);

        public async Task Delete(int id) =>
            await _client.DeleteAsync($"documents/{id}");
    }
}
