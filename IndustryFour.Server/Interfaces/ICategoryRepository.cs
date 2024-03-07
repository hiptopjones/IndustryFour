using IndustryFour.Server.Models;

namespace IndustryFour.Server.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>, IDisposable
    {
    }
}
