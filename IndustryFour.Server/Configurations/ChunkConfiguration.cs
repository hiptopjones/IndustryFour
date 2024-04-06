using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class ChunkConfiguration : IEntityTypeConfiguration<Chunk>
{
    public void Configure(EntityTypeBuilder<Chunk> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .IsRequired();

        builder.Property(b => b.Content)
            .IsRequired();

        builder.Property(b => b.Embedding)
            .IsRequired()
            .HasColumnType("vector(768)");

        builder.Property(b => b.DocumentId)
            .IsRequired();

        builder.Property(b => b.NextChunkId)
            .IsRequired();

        builder.Property(b => b.PreviousChunkId)
            .IsRequired();

        builder.ToTable("chunks");
    }
}
