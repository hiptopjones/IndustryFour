using IndustryFour.Server.Models;

namespace IndustryFour.Server.Repositories
{
    public interface IQuestionRepository : IRepository<Question>, IDisposable
    {
        Task<Question> GetByQuestionText(string questionText);
    }
}
