﻿using IndustryFour.Server.Context;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;

namespace IndustryFour.Server.Repositories
{
    public class ChunkRepository : Repository<Chunk>, IChunkRepository
    {
        public ChunkRepository(DocumentStoreDbContext context) : base(context)
        {
        }

        public async Task<bool> RemoveByDocumentId(int documentId)
        {
            DbSet.RemoveRange(Db.Chunks.Where(x => x.DocumentId == documentId));
            return true;
        }

        public async Task<IEnumerable<Chunk>> GetChunksByDistance(Vector vector, int k)
		{
			// TODO: Should do something like table-splitting to avoid bringing in the embedding vector here?

            var chunks = await DbSet
				.AsNoTracking()
                .Include(x => x.Document)
                .ThenInclude(x => x.Category)
                .OrderBy(x => x.Embedding.L2Distance(vector))
                .Take(k)
                .ToListAsync();

            return chunks;
		}
    }
}
