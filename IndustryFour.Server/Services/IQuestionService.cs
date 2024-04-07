using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services;

public interface IQuestionService : IDisposable
{
    Task<IEnumerable<Question>> GetAll();
    Task<Question> GetById(int id);
    Task<Question> Add(Question question);
    Task<Question> Update(Question question);
    Task<bool> Remove(Question question);

    Task<Question> GetByQuestionText(string questionText);
}
