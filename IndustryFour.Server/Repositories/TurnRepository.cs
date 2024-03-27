using IndustryFour.Server.Context;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustryFour.Server.Repositories
{
    public class TurnRepository : Repository<Turn>, ITurnRepository
    {
        public TurnRepository(DocumentStoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Turn>> GetByConversationId(int conversationId)
        {
            var turns = await DbSet
                .AsNoTracking()
                .Include(x => x.Conversation)
                .Where(x => x.ConversationId == conversationId)
                .ToListAsync();

            return turns;
        }
    }
}
