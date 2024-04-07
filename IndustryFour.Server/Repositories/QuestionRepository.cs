using IndustryFour.Server.Context;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustryFour.Server.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(DocumentStoreDbContext context) : base(context)
        {
        }

        public async Task<Question> GetByQuestionText(string questionText)
        {
            var question = await DbSet
                .AsNoTracking()
                .Where(x => x.QuestionText == questionText)
                .FirstOrDefaultAsync();

            return question;
        }
    }
}
