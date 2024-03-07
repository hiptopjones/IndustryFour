using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustryFour.Server.Context;

public class DocumentStoreDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }

    public DocumentStoreDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(150)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentStoreDbContext).Assembly);

        // Disable cascaded deletion
        // https://henriquesd.medium.com/creating-an-application-from-scratch-using-net-core-and-angular-part-2-95e67eebadde
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }
}
