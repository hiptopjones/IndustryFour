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

    Task<IEnumerable<Chunk>> GetByDocumentId(int documentId);
    Task<bool> RemoveByDocumentId(int documentId);
    
    Task<IEnumerable<ChunkMatch>> GetByDistance(Vector vector, int k);
}
