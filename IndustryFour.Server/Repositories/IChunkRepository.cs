using IndustryFour.Server.Models;
using Pgvector;

namespace IndustryFour.Server.Repositories
{
    public interface IChunkRepository : IRepository<Chunk>, IDisposable
    {
        Task<IEnumerable<Chunk>> GetChunksByDistance(Vector vector, int k);
	}
}
