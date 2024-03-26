using IndustryFour.Shared.Dtos.Chunk;
using System.Net.Http.Json;

namespace IndustryFour.Client.HttpRepository
{
    public class ChunkHttpRepository : IChunkHttpRepository
    {
        private readonly HttpClient _client;
        
        public ChunkHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ChunkResultDto>> GetAll()
        {
            var chunks = await _client.GetFromJsonAsync<List<ChunkResultDto>>("chunks");
            return chunks;
        }

		public async Task<ChunkResultDto> GetById(int id)
		{
			var chunks = await _client.GetFromJsonAsync<ChunkResultDto>($"chunks/{id}");
			return chunks;
		}

		public async Task<List<ChunkResultDto>> GetByDocumentId(int documentId)
		{
			var chunks = await _client.GetFromJsonAsync<List<ChunkResultDto>>($"chunks/document/{documentId}");
			return chunks;
		}
	}
}
