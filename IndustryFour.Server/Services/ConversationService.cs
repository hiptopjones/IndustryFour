using IndustryFour.Server.Models;
using IndustryFour.Server.Repositories;

namespace IndustryFour.Server.Services;

public class ConversationService : IConversationService
{
    private readonly IConversationRepository _conversationRepository;

    public ConversationService(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<IEnumerable<Conversation>> GetAll()
    {
        return await _conversationRepository.GetAll();
    }

    public async Task<Conversation> GetById(int id)
    {
        return await _conversationRepository.GetById(id);
    }

    public async Task<Conversation> Add(Conversation conversation)
    {
        await _conversationRepository.Add(conversation);
        return conversation;
    }

    public async Task<Conversation> Update(Conversation conversation)
    {
        await _conversationRepository.Update(conversation);
        return conversation;
    }

    public async Task<bool> Remove(Conversation conversation)
    {
        await _conversationRepository.Remove(conversation);		
		return true;
    }

    public void Dispose()
    {
        _conversationRepository?.Dispose();
    }
}