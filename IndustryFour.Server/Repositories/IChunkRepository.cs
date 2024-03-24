using IndustryFour.Server.Models;
using Pgvector;

namespace IndustryFour.Server.Repositories
{
    public interface IChunkRepository : IRepository<Chunk>, IDisposable
    {
        Task<bool> RemoveByDocumentId(int documentId);
        Task<IEnumerable<Chunk>> GetChunksByDistance(Vector vector, int k);
	}
}
