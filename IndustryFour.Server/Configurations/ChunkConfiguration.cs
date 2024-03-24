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
			.IsRequired()
			.HasColumnName("id");

        builder.Property(b => b.Content)
            .IsRequired()
			.HasColumnName("content");

		builder.Property(b => b.Embedding)
			.IsRequired()
			.HasColumnType("vector(768)")
			.HasColumnName("embedding");

		builder.Property(b => b.DocumentId)
			.IsRequired()
			.HasColumnName("document_id");

		builder.ToTable("chunks");
    }
}
