using IndustryFour.Server.Models;
using IndustryFour.Server.Repositories;

namespace IndustryFour.Server.Services;

public class TurnService : ITurnService
{
    private readonly ITurnRepository _turnRepository;

    public TurnService(ITurnRepository turnRepository)
    {
        _turnRepository = turnRepository;
    }

    public async Task<IEnumerable<Turn>> GetAll()
    {
        return await _turnRepository.GetAll();
    }

    public async Task<Turn> GetById(int id)
    {
        return await _turnRepository.GetById(id);
    }

    public async Task<Turn> Add(Turn turn)
    {
        await _turnRepository.Add(turn);
        return turn;
    }

    public async Task<Turn> Update(Turn turn)
    {
        await _turnRepository.Update(turn);
        return turn;
    }

    public async Task<bool> Remove(Turn turn)
    {
        await _turnRepository.Remove(turn);
        return true;
    }

    public async Task<IEnumerable<Turn>> GetByConversationId(int conversationId)
    {
        var turns = await _turnRepository.GetByConversationId(conversationId);
        return turns;
    }

    public void Dispose()
    {
        _turnRepository?.Dispose();
    }
}