using IndustryFour.Server.Context;
using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustryFour.Server.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DocumentStoreDbContext context) : base(context)
        {
        }

        public override async Task<List<Document>> GetAll()
        {
            return await DbSet
                .AsNoTracking()
                .Include(d => d.Category)
                .OrderBy(d => d.Title)
                .ToListAsync();
        }

        public override async Task<Document> GetById(int id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Document>> GetByCategoryId(int categoryId)
        {
            return await Search(d => d.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Document>> SearchDocumentWithCategory(string searchedValue)
        {
            return await DbSet.AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.Title.Contains(searchedValue) ||
                            d.Author.Contains(searchedValue) ||
                            d.Description.Contains(searchedValue) ||
                            d.Category.Name.Contains(searchedValue))
                .ToListAsync();
        }
    }
}
