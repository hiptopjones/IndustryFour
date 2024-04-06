using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IndustryFour.Server.Context;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<DocumentStoreDbContext>
{
    public DocumentStoreDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DocumentStoreDbContext>();        
		return new DocumentStoreDbContext(builder.Options);
    }
}
