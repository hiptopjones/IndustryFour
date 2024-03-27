using IndustryFour.Server.Models;

namespace IndustryFour.Server.Repositories
{
    public interface ITurnRepository : IRepository<Turn>, IDisposable
    {
        Task<IEnumerable<Turn>> GetByConversationId(int conversationId);
	}
}
