using IndustryFour.Server.Models;
using System.Linq.Expressions;

namespace IndustryFour.Server.Repositories;

public interface IRepository<T> where T : Entity
{
    Task Add(T entity);
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task Update(T entity);
    Task Remove(T entity);
    Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
    Task<int> SaveChanges();
}
