using IndustryFour.Server.Models;
using Pgvector;

namespace IndustryFour.Server.Repositories
{
    public interface IChunkRepository : IRepository<Chunk>, IDisposable
    {
        Task<IEnumerable<Chunk>> GetByDocumentId(int documentId);
        Task<bool> RemoveByDocumentId(int documentId);
        Task<IEnumerable<ChunkMatch>> GetByDistance(Vector vector, int k);
	}
}
