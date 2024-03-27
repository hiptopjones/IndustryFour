using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services;

public interface IConversationService : IDisposable
{
    Task<IEnumerable<Conversation>> GetAll();
    Task<Conversation> GetById(int id);
    Task<Conversation> Add(Conversation conversation);
    Task<Conversation> Update(Conversation conversation);
    Task<bool> Remove(Conversation conversation);
}
