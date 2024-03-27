using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services;

public interface ITurnService : IDisposable
{
    Task<IEnumerable<Turn>> GetAll();
    Task<Turn> GetById(int id);
    Task<Turn> Add(Turn turn);
    Task<Turn> Update(Turn turn);
    Task<bool> Remove(Turn turn);

    Task<IEnumerable<Turn>> GetByConversationId(int conversationId);
}
