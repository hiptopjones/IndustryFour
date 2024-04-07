using IndustryFour.Server.Models;
using IndustryFour.Server.Repositories;

namespace IndustryFour.Server.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        return await _questionRepository.GetAll();
    }

    public async Task<Question> GetById(int id)
    {
        return await _questionRepository.GetById(id);
    }

    public async Task<Question> Add(Question question)
    {
        await _questionRepository.Add(question);
        return question;
    }

    public async Task<Question> Update(Question question)
    {
        await _questionRepository.Update(question);
        return question;
    }

    public async Task<bool> Remove(Question question)
    {
        await _questionRepository.Remove(question);
        return true;
    }

    public void Dispose()
    {
        _questionRepository?.Dispose();
    }

    public async Task<Question> GetByQuestionText(string questionText)
    {
        return await _questionRepository.GetByQuestionText(questionText);
    }
}