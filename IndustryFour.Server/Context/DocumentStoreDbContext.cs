using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using static OllamaSharp.OllamaApiClient;

namespace IndustryFour.Server.Context;

public class DocumentStoreDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
	public DbSet<Document> Documents { get; set; }
	public DbSet<Chunk> Chunks { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Turn> Turns { get; set; }

    public DocumentStoreDbContext(DbContextOptions options)
        : base(options)
    {
    }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json").Build();

		var connectionString = configuration.GetConnectionString("sqlConnection");
		optionsBuilder.UseNpgsql(connectionString, x => x.UseVector());
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.HasPostgresExtension("vector");

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
