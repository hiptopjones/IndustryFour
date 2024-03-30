using IndustryFour.Server.Context;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using System.Linq;

namespace IndustryFour.Server.Repositories
{
    public class ChunkRepository : Repository<Chunk>, IChunkRepository
    {
        public ChunkRepository(DocumentStoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Chunk>> GetByDocumentId(int documentId)
        {
            var chunks = await DbSet
                .AsNoTracking()
                .Include(x => x.Document)
                .Where(x => x.DocumentId == documentId)
                .ToListAsync();

            return chunks;
        }

        public async Task<bool> RemoveByDocumentId(int documentId)
        {
            DbSet.RemoveRange(Db.Chunks.Where(x => x.DocumentId == documentId));
            return await Task.FromResult(true);
        } 

        public async Task<IEnumerable<ChunkMatch>> GetByDistance(Vector vector, int k)
		{
            var chunkMatches = await DbSet
                .AsNoTracking()
                .Include(x => x.Document)
                .ThenInclude(x => x.Category)
                .Select(x => new ChunkMatch { Chunk = x, Distance = x.Embedding.L2Distance(vector) })
                .OrderBy(x => x.Distance)
                .Take(k)
                .ToListAsync();
;
            return chunkMatches;
		}
    }
}
