using IndustryFour.Server.Models;
using IndustryFour.Server.Repositories;
using Pgvector;

namespace IndustryFour.Server.Services;

public class ChunkService : IChunkService
{
    private readonly IChunkRepository _chunkRepository;

    public ChunkService(IChunkRepository chunkRepository)
    {
        _chunkRepository = chunkRepository;
    }

    public async Task<IEnumerable<Chunk>> GetAll()
    {
        return await _chunkRepository.GetAll();
    }

    public async Task<Chunk> GetById(int id)
    {
        return await _chunkRepository.GetById(id);
    }

    public async Task<Chunk> Add(Chunk chunk)
    {
        await _chunkRepository.Add(chunk);
        return chunk;
    }

    public async Task<Chunk> Update(Chunk chunk)
    {
        await _chunkRepository.Update(chunk);
        return chunk;
    }

    public async Task<bool> Remove(Chunk chunk)
    {
        await _chunkRepository.Remove(chunk);
        return true;
    }

	public async Task<IEnumerable<Chunk>> GetChunksByDistance(Vector vector, int k)
    {
        var chunks = await _chunkRepository.GetChunksByDistance(vector, k);
        return chunks;
    }

	public void Dispose()
    {
        _chunkRepository?.Dispose();
    }
}