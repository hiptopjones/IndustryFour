using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;

namespace IndustryFour.Server.Context;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<DocumentStoreDbContext>
{
    public DocumentStoreDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DocumentStoreDbContext>();        
		return new DocumentStoreDbContext(builder.Options);
    }
}
