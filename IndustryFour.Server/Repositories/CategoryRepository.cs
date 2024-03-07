using IndustryFour.Server.Context;
using IndustryFour.Server.Interfaces;
using IndustryFour.Server.Models;

namespace IndustryFour.Server.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DocumentStoreDbContext context) : base(context)
        {
        }
    }
}
