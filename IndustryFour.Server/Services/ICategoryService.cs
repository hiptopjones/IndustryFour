using IndustryFour.Server.Models;
using System.Reflection.Metadata;

namespace IndustryFour.Server.Services;

public interface ICategoryService : IDisposable
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(int id);
    Task<Category> Add(Category category);
    Task<Category> Update(Category category);
    Task<bool> Remove(Category category);
    Task<IEnumerable<Category>> Search(string categoryTitle);
}
