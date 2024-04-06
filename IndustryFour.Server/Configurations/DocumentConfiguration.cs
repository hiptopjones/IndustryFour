using IndustryFour.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndustryFour.Server.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(b => b.Id);

		builder.Property(b => b.Id)
			.IsRequired();

        builder.Property(b => b.Title)
            .IsRequired();

		builder.Property(b => b.Author)
			.IsRequired();

		builder.Property(b => b.Description)
			.IsRequired();

		builder.Property(b => b.ContentUrl)
			.IsRequired();

		builder.Property(b => b.SourceUrl);

		builder.Property(b => b.PublishDate)
			.HasConversion
			(
				src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
				dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
			);

        builder.Property(b => b.CategoryId)
			.IsRequired();

		builder.ToTable("documents");
    }
}
