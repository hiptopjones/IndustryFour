using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired();

        builder.Property(b => b.Description)
            .IsRequired(false)
            .HasColumnType("varchar(350)");

        builder.Property(b => b.Content)
            .IsRequired();

        builder.Property(b => b.PublishDate)
            .IsRequired();

        builder.Property(b => b.CategoryId)
            .IsRequired();

        builder.ToTable("Documents");
    }
}
