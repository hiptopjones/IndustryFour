using IndustryFour.Server.Context;
using IndustryFour.Server.Models;

namespace IndustryFour.Server.Repositories
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        public ConversationRepository(DocumentStoreDbContext context) : base(context)
        {
        }
    }
}
