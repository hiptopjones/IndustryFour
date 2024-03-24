using IndustryFour.Server.Models;
using Pgvector;

namespace IndustryFour.Server.Services;

public interface IChunkService : IDisposable
{
    Task<IEnumerable<Chunk>> GetAll();
    Task<Chunk> GetById(int id);
    Task<Chunk> Add(Chunk chunk);
    Task<Chunk> Update(Chunk chunk);
    Task<bool> Remove(Chunk chunk);

    Task<bool> RemoveByDocumentId(int documentId);
    Task<IEnumerable<Chunk>> GetChunksByDistance(Vector vector, int k);
}
