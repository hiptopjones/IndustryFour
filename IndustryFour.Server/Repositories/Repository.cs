using IndustryFour.Server.Context;
using IndustryFour.Server.Interfaces;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IndustryFour.Server.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DocumentStoreDbContext Db;

        protected readonly DbSet<T> DbSet;

        protected Repository(DocumentStoreDbContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(T entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(T entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
