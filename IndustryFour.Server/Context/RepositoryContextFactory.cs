using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace IndustryFour.Server.Context;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<DocumentStoreDbContext>
{
    public DocumentStoreDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        var builder = new DbContextOptionsBuilder<DocumentStoreDbContext>()
            .UseSqlite(configuration.GetConnectionString("sqlConnection"));

        return new DocumentStoreDbContext(builder.Options);
    }
}
