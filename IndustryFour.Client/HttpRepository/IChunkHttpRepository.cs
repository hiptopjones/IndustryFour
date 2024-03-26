using IndustryFour.Shared.Dtos.Chunk;

namespace IndustryFour.Client.HttpRepository
{
	public interface IChunkHttpRepository
    {
        Task<List<ChunkResultDto>> GetAll();
		Task<ChunkResultDto> GetById(int id);
		Task<List<ChunkResultDto>> GetByDocumentId(int documentId);
	}
}
